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

task :test do #=> :build do
  gallio_path = GALLIO_PATH + "Gallio.Echo.exe"
  if (File.exist?(gallio_path))
	print "starting tests..."
    sh "\"#{gallio_path}\" #{BUILD_DIR}/Tests.Math.dll /working-directory:bin /report-directory:build /report-type:Html" #/show-reports
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
