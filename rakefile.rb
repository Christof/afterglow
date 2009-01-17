require 'rexml/document'
include REXML

BUILD_DIR = "bin"
GALLIO_PATH = "C:/Program Files/Gallio/bin/"
SOLUTION_NAME = "TheNewEngine.sln"

task :default => [:build]
#task :all => [ :removeBuildDir, :removeVsDirs, :default]

task :clear do
	sh "cls"
end

class MsBuild
	@@DOT_NET_PATH = "#{ENV['windir']}/Microsoft.net/framework/v3.5/"

	def self.build(solution_name, configuration = "Debug", additional_options = "")
		sh "#{@@DOT_NET_PATH}msbuild.exe /property:Configuration=#{configuration} #{solution_name} " +
			" /maxcpucount:2 #{additional_options}"
	end
end

task :removeBuildDir => :clear do
	puts "removing build directory (#{BUILD_DIR})"
	if (File.exist?(BUILD_DIR))
		sh "rd /s /q #{BUILD_DIR}"
	end
end

directory BUILD_DIR

task :build => [:removeBuildDir, BUILD_DIR] do
	MsBuild.build(SOLUTION_NAME)
end

task :test do#=> :build do
	gallio_path = GALLIO_PATH + "Gallio.Echo.exe"
	if (File.exist?(gallio_path))
		puts "starting tests..."
		assemblies = Dir["#{BUILD_DIR}/Tests.*.dll"].join(" ")
		cmd = "\"#{gallio_path}\" #{assemblies} "#/f:not(CategoryName:API_Examples) " +#/filter:not(Type:TriangleWithTexture) " + 
			" /working-directory:bin /report-directory:build /report-type:Html" #/show-reports
		puts cmd
		begin
			sh cmd
		rescue => err
			puts err
		end
	end
	
	dir = Dir.new("build")
	
	entries = dir.entries.find_all do	|filename|
		/html/ =~ filename
	end
	
	newest_file = entries.max do |a, b|
		File.new("build/#{a}").atime <=> File.new("build/#{b}").atime
	end
	
	print "\nnewest file #{newest_file}\n"
	
	replace_underlines("build/#{newest_file}")
end

def replace_underlines(path)
	file = File.new(path, "r")
	lines = file.readlines
	file.close
	
	lines.each do |line|
		line.gsub!("_", " ")
	end
	
	file = File.new(path, "w")
	lines.each do |line|
		file.write(line)
	end
	file.close
end

task :fixRef do
	#get all dependency names
	puts 'dependencies'
	dependencies = {}
	Dir['thirdparty/**/*.dll'].each do |path|
		if (path.include?('x64') == false) # don't support x64 currently
			dependencies[File.basename(path, '.dll')] = '..\\..\\' + path.gsub('/', '\\')
		end
	end
	
	#dependencies.each {|k,v| puts "#{k}: #{v}"}
	
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
