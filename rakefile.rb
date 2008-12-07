require 'rexml/document'
include REXML

BUILD_DIR = "bin"
DOT_NET_PATH = "#{ENV['windir']}\\Microsoft.net\\framework\\v3.5\\"
GALLIO_PATH = "C:\\Program Files\\Gallio\\bin\\"
SOLUTION_NAME = "TheNewEngine.sln"

task :default => [:build]
#task :all => [ :removeBuildDir, :removeVsDirs, :default]

task :removeBuildDir => :clear do
  sh "echo removeBuildDir"
  rm_rf(BUILD_DIR)
end

directory BUILD_DIR

task :build => [BUILD_DIR] do
  sh "#{DOT_NET_PATH}msbuild.exe /property:Configuration=Release #{SOLUTION_NAME}"
end

task :test => :build do
  gallio_path = GALLIO_PATH + "Gallio.Echo.exe"
  if (File.exist?(gallio_path))
		print "starting tests..."
    cmd = "\"#{gallio_path}\" #{BUILD_DIR}/Tests.Graphics.SlimDX.dll /working-directory:bin /report-directory:build /report-type:Html" #/show-reports
		print cmd
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
	File.new("build\\#{a}").atime <=> File.new("build\\#{b}").atime
  end
  
  print "\nnewest file #{newest_file}\n"
  
  replace_underlines("build\\#{newest_file}")
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
		#s = ""
		doc.write(file, 2)
		#s.gsub('\'', '\"')
		#doc.write
		file.close		
		break
	end		
end
