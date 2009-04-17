using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Afterglow.Applications.TestRunner
{

    public class TestRunnerStorage
    {
        private Dictionary<Type, List<KeyValuePair<MethodInfo, String>>> mStorage;
        private Dictionary<string, List<Type>> mAssemblies;

        private Dictionary<string, List<Type>> mCategoryIndex;

        public TestRunnerStorage()
        {
            mStorage = new Dictionary<Type, List<KeyValuePair<MethodInfo, string>>>();
            mAssemblies = new Dictionary<string, List<Type>>();
            mCategoryIndex = new Dictionary<string, List<Type>>();
        }

        public KeyValuePair<MethodInfo, string>[] this[Type type]
        {
            get
            {
                if (!mStorage.ContainsKey(type))
                {
                    return null;
                }
                return mStorage[type].ToArray();
            }
        }

        public Type[] TestFixtureTypes
        {
            get
            {
                return mStorage.Keys.ToArray();
            }
        }

        public string[] AssemblyNames
        {
            get
            {
                return mAssemblies.Keys.ToArray();
            }
        }

        //todo: abstract this
        public void AddTestMethod(string assemblyName, Type testFixtureType,
            MethodInfo testMethodInfo, string category)
        {
            if (!mStorage.ContainsKey(testFixtureType))
            {
                mStorage.Add(testFixtureType, new List<KeyValuePair<MethodInfo, string>>());
            }

            var list = mStorage[testFixtureType];
            var entry = new KeyValuePair<MethodInfo, string>(testMethodInfo, category);
            if (!list.Contains(entry))
            {
                list.Add(entry);
            }

            if (!mAssemblies.ContainsKey(assemblyName))
            {
                mAssemblies.Add(assemblyName, new List<Type>(new[] { testFixtureType, }));
            }
            else
            {
                var typeList = mAssemblies[assemblyName];
                if (!typeList.Contains(testFixtureType))
                {
                    typeList.Add(testFixtureType);
                }
            }

            if(!mCategoryIndex.ContainsKey(category))
            {
                mCategoryIndex.Add(category, new List<Type>(new [] {testFixtureType,}));
            }
            else
            {
                var typeList = mCategoryIndex[category];
                if(!typeList.Contains(testFixtureType))
                {
                    typeList.Add(testFixtureType);
                }
            }
        }

        public void Foreach(System.Action<string, Type, MethodInfo, string>  action)
        {
            foreach (var typeMethodInfoAndCategory in mStorage)
            {
                var type = typeMethodInfoAndCategory.Key;
                foreach (var methodInfoCategory in typeMethodInfoAndCategory.Value)
                {
                    var methodInfo = methodInfoCategory.Key;
                    var category = methodInfoCategory.Value;
                    var assembly = type.Assembly.FullName;

                    action(assembly, type, methodInfo, category);
                }
            }
        }

        public void WriteToTrace()
        {
            Trace.WriteLine("#Begin TestRunnerStorage");
            this.Foreach(
                (assembly, type, method, category) =>
                Trace.WriteLine(string.Format("{0}, {1}, {2}, {3}",
                assembly, type, method, category)));
            Trace.WriteLine("#End TestRunnerStorage");
        }
    }
}
