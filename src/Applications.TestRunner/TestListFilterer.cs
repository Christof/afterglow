using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Afterglow.Applications.TestRunner
{
    /// <summary>
    /// Feals like a huge OR-FILTER with lazy bindings
    /// </summary>
    public class TestListFilterer
    {
        private Func<string[]> mAssemblies;

        private Func<string[]> mFixtureTypes;

        private Func<string[]> mTestFunctions;

        private Func<string[]> mCategories;

        public TestListFilterer(Func<string[]> assemblies,
            Func<string[]> fixtureTypes,
            Func<string[]> testFunctions,
            Func<string[]> categories)
        {
            mAssemblies = assemblies;
            mFixtureTypes = fixtureTypes;
            mTestFunctions = testFunctions;
            mCategories = categories;
        }

        public bool Check(string assembly, string fixtureType,
            string testFunction, string category)
        {
            return mAssemblies().Contains(assembly) ||
                mFixtureTypes().Contains(fixtureType) ||
                    mTestFunctions().Contains(testFunction) ||
                        mCategories().Contains(category);
        }
    }
}
