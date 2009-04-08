using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Gallio;

namespace Afterglow.Applications.TestRunner
{

    public class TestRunnerStorage
    {
        private Dictionary<Type, List<Pair<MethodInfo, String>>> mStorage;
        private Dictionary<string, List<Type>> mAssemblies;

        private Dictionary<string, List<Type>> mCategoryIndex;

        public TestRunnerStorage()
        {
            mStorage = new Dictionary<Type, List<Pair<MethodInfo, string>>>();
            mAssemblies = new Dictionary<string, List<Type>>();
            mCategoryIndex = new Dictionary<string, List<Type>>();
        }

        public Pair<MethodInfo, string>[] this[Type type]
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
                mStorage.Add(testFixtureType, new List<Pair<MethodInfo, string>>());
            }

            var list = mStorage[testFixtureType];
            var entry = new Pair<MethodInfo, string>(testMethodInfo, category);
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
                    var methodInfo = methodInfoCategory.First;
                    var category = methodInfoCategory.Second;
                    var assembly = type.Assembly.FullName;

                    action(assembly, type, methodInfo, category);
                }
            }
        }
    }
}
