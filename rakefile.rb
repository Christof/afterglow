buildDir = "bin"
DOT_NET_PATH = "C:\\Windows\\Microsoft.net\\framework\\v3.5\\"

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
  sh "echo build"
  sh DOT_NET_PATH + "msbuild.exe /property:Configuration=Release TheNewEngine.sln"
end
