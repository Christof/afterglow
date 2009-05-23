require 'rexml/document'
require 'net/http'
include REXML

BUILD_DIR = "bin"
SOLUTION_NAME = "Afterglow.sln"

task :default => [:build]
#task :all => [ :removeBuildDir, :removeVsDirs, :default]

desc "Cleans the screen."
task :clear do
	sh "cls"
end

class MsBuild
	@@DOT_NET_PATH = "#{ENV['windir']}/Microsoft.net/framework/v4.0.20506/"

	def self.build(solution_name, configuration = "Debug", additional_options = "")
		sh "#{@@DOT_NET_PATH}msbuild.exe /property:Configuration=#{configuration} #{solution_name} " +
			" /maxcpucount:2 #{additional_options}"
	end
end

class Gallio
	@@GALLIO_PATH = "/thirdparty/tools/Gallio/Gallio.Echo.exe"
	
	def initialize(build_dir, report_dir)
		@build_dir = build_dir
		@report_dir = report_dir
		@show_reports = false
		@filter = ""
	end
	
	def assemblies()
		assemblies = Dir["#{@build_dir}/Tests.*.dll"].join(" ")
	end
	
	def exclude_categories(categories)
		@filter = " \"/f:exclude Category:#{categories.join(", ")}\" "
	end
	
	def show_reports()
		@show_reports = true
	end
	
	def show_reports_option()
		if @show_reports
			"/show-reports"
		end
	end
	
	 def run()
		sh "\"#{Dir.getwd + @@GALLIO_PATH}\" #{assemblies}  /working-directory:#{@build_dir} /report-name-format:test-report " + 
			" /report-directory:#{@report_dir} /report-type:Html #{show_reports_option} " + @filter do |ok, res|
			if ! ok
			   puts "error while exec gallio: #{res.exitstatus}"
			end
		end
	 end
end

desc "Removes the build directory."
task :removeBuildDir => :clear do
	puts "removing build directory (#{BUILD_DIR})"
	if (File.exist?(BUILD_DIR))
		sh "rd /s /q #{BUILD_DIR}"
	end
end

directory BUILD_DIR

desc "Builds the solution."
task :build => [:removeBuildDir, BUILD_DIR] do
	MsBuild.build(SOLUTION_NAME)
end

desc "Runs all unit tests."
task :test do
	report_dir = "build"
	gallio = Gallio.new(BUILD_DIR, report_dir)
	gallio.exclude_categories(["API_Examples", "Examples", "Manual", "Direct3D10"])
	if (ENV['show_report'] == 'true' || ENV['sr'] == 'true')
		gallio.show_reports
	end
	gallio.run
	
	file_substitute("#{report_dir}/#{'test-report.html'}", "_", " ")
end

desc "Fixes references which use absolute paths or don't use the HintPath at all."
task :fix_references do
	#get all dependency names
	puts 'dependencies'
	dependencies = {}
	Dir['thirdparty/**/*.dll'].each do |path|
		dependencies[File.basename(path, '.dll')] = '..\\..\\' + path.gsub('/', '\\')
	end
	
	#get all projects.
	Dir['src/**/*.csproj'].each do |path|
		puts path
		project_file = File.new(path, 'r')
		doc = Document.new(project_file)
		project_file.close
		
		doc.elements.each('Project/ItemGroup/Reference') do |e|
			include_element = e.attributes['Include']
			name = include_element.to_s.split(',')[0]
			e.delete_element('HintPath')
			
			if (dependencies.has_key?(name))
				hint_element = Element.new('HintPath')
				hint_element.text = dependencies[name]
				e.add_element(hint_element)
			end
			
			e.elements.each('HintPath') do |element|
				puts "\t" + element.get_text.to_s
			end
		end
				
		file = File.new(path, 'w')
		s = ""
		doc.write(s)
		s.gsub!('\'', '"')
		s.gsub!('&apos;', '\'')
		file.write(s)
		file.close
	end
end

desc "Download dependencies."
task :download_dependencies do
	require 'yaml'
	file = File.new("thirdparty/dependencies.yaml")
	hash = YAML.load(file)
	file.close
	
	require 'open-uri'
	hash.each do |name, link|
		puts "Do you want to download #{name}? [y] Yes [n] No"
		str = STDIN.gets #gets alone doesn't work
		str.chomp!
		if str == "n" then
			next
		end
		
		if link.is_a? Hash then
			puts "Which version of #{name} do you want to download? [1] x86 [2] x64"
			str = STDIN.gets
			if str == "1" then
				link = link["x86"]
				name += "_x86"
			else
				link = link["x64"]
				name += "_x64"
			end
		end
		puts "Downloading #{name}..."
		filename = name + link[link.rindex(".")..link.length-1]
		file = open(filename, "wb")
		file.write(open(link).read)
		file.close
	end
end

desc "Task for teamcity test config"
task :teamcity => [:build, :test] do

    file = "build/test-report.html"
	file_substitute(file, "test-report/", "http://joe.dragonhill.cc:89/afterglow/")
	puts "##teamcity[publishArtifacts '#{file}']"
end

def file_substitute(path, find, replace)
	file = File.new(path, "r")
	lines = file.readlines
	file.close
	
	lines.each do |line|
		line.gsub!(find, replace)
	end
	
	file = File.new(path, "w")
	lines.each do |line|
		file.write(line)
	end
	file.close
end