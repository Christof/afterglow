using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using WysiwygEditor.CodeAnalyzer;

namespace Tests.Application.WysiwygEditor
{
    [TestFixture]
    public class Test_CodeExecutor
    {
        [Test]
        public void Test_script_nothing()
        {
            new CodeExecutor().Execute(string.Empty);
        }

        [Test]
        public void Test_script_define_int()
        {
            new CodeExecutor().Execute("int i = int.MinValue;");
        }

        [Test]
        [Ignore("Feature: 'var keyword' not available at the moment!")]
        public void Test_script_define_int_using_var_keyword_fails()
        {
            var systemAssembly = @"C:\Programme\Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll";
            The.Action(
                () =>
                new CodeExecutor().Execute("var i = int.MinValue;",
                systemAssembly.ToArrayWithOneElement())
                ).ShouldThrow<System.OperationCanceledException>();
        }

        [Test]
    //    [Ignore("Feature: 'usign' not available at the moment!")]
        public void Test_script_define_code_executor()
        {

            var assembly = "Applications.WysiwygEditor.dll";
            new CodeExecutor().Execute("new WysiwygEditor.CodeAnalyzer.CodeExecutor();",
               assembly.ToArrayWithOneElement());
        }

        [Test]
        public void Test_script_hello_world()
        {
            try
            {
                var startupPath = new FileInfo(Assembly.GetExecutingAssembly().Location);
                var systemFile = Path.Combine(startupPath.Directory.FullName, "System.dll");
                var referencedAssemblies = systemFile.ToArrayWithOneElement();
                new CodeExecutor().Execute("Console.WriteLine(\"Hello World!\"); ",
                    referencedAssemblies);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}
