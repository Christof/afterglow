buildDir = "bin"
DOT_NET_PATH = "C:\\Windows\\Microsoft.net\\framework\\v3.5\\"
GALLIO_PATH = "C:\\Program Files\\Gallio\\bin\\"

task :default => [:build]
#task :all => [:clear, :removeBuildDir, :removeVsDirs, :default]

task :clear do
  #sh "clear"
end

task :removeBuildDir => :clear do
  sh "echo removeBuildDir"
  rm_rf(buildDir)
end

directory buildDir

task :build => [buildDir] do
  sh DOT_NET_PATH + "msbuild.exe /property:Configuration=Release TheNewEngine.sln"
end

task :test do #=> :build do
  sh GALLIO_PATH + "Gallio.Echo.exe bin/Tests.Math.dll /working-directory:bin /report-directory:build /report-type:Html /show-reports"
end

task :t do
  sh GALLIO_PATH + "Gallio.Echo.exe TheNewEngine.gallio"
end

task :env do
    ENV.each {|k,v| puts "#{k}=#{v}" }
end
