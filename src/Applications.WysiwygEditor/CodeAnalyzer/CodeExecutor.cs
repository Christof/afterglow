using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace WysiwygEditor.CodeAnalyzer
{
    public class CodeExecutor
    {
        private const string STUB = "static class Program{ public static void Main(){ {0} }}";
        public void Execute(string src)
        {
            this.Execute(src, new string[0]);
        }
        public void Execute(string src, string[] referencedAssemblyNames)
        {
            var fileSource = STUB.Replace("{0}", src);

            ICodeCompiler compiler = (new CSharpCodeProvider()).CreateCompiler();
            var compileParams = new CompilerParameters();
            compileParams.IncludeDebugInformation = true;
            compileParams.GenerateExecutable = compileParams.GenerateInMemory = true;
     //       compileParams.ReferencedAssemblies.AddRange(referencedAssemblyNames);
            if(referencedAssemblyNames.Length > 0)
            { 
                var startupPath = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
                startupPath = startupPath.Trim('\\');
                compileParams.CompilerOptions = string.Format("/lib:\"{0}\" /reference:\"{1}\" ",
                    startupPath,
                    referencedAssemblyNames[0]);
            }

            var compilerResult = compiler.CompileAssemblyFromSource(compileParams, fileSource);
            if(compilerResult.Errors.HasErrors)
            {
                var message = string.Empty;
                foreach (var error in compilerResult.Errors)
                {
                    Trace.WriteLine(error.ToString());
                    message += error.ToString() + '\n';
                }
                throw new OperationCanceledException("Compilation failed: " + message);
            }

            var types = compilerResult.CompiledAssembly.GetTypes();
            var type = types[0];
            var method = type.GetMethod("Main", new Type[0]);
            method.Invoke(null, new object[0]);
        }
    }
}
