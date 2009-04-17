using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Afterglow.Infrastructure;
using MbUnit.Framework;

namespace Afterglow.Applications.TestRunner
{
    public class TestAssemblyLoader
    {
        /// <summary>
        /// Loads all information of the given asembly to the given storage
        /// </summary>
        /// <param name="assemblyFileName">filename of the assembly to scan</param>
        /// <param name="storage">storage to fill</param>
        public void LoadToStorage(string assemblyFileName, TestRunnerStorage storage)
        {
            var assemblyFile = new FileInfo(assemblyFileName);
            if (assemblyFile.Exists)
            {
                Assembly assembly = Assembly.LoadFrom(assemblyFile.FullName);
                Type[] types = assembly.GetExportedTypes();
                foreach (Type type in types)
                {
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

                    foreach (MethodInfo methodInfo in methods)
                    {
                        try
                        {
                            RowAttribute[] rowAttributes = methodInfo.GetCustomAttributes<RowAttribute>();
                            if (rowAttributes.Length > 0)
                                continue;

                            TestAttribute[] testAttributes = methodInfo.GetCustomAttributes<TestAttribute>();
                            if (testAttributes.Length == 1)
                            {
                                string category = null;
                                CategoryAttribute[] categoryAttributes =
                                    methodInfo.GetCustomAttributes<CategoryAttribute>();
                                if (categoryAttributes.Length == 0)
                                {
                                    storage.AddTestMethod(assemblyFile.Name, type, methodInfo, null);
                                }
                                foreach (CategoryAttribute categoryAttribute in categoryAttributes)
                                {
                                    storage.AddTestMethod(assemblyFile.Name, type, methodInfo,
                                                          categoryAttribute.Category);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.GetType().Name);
                            Trace.WriteLine(ex.Message);
                        }
                    }
                }
            }
            //method end
        }
    }
}