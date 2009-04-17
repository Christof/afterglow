using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Afterglow.Applications.TestRunner
{
    public class TestListRunner
    {
        private readonly TestRunnerStorage mStorage;

        private readonly TestListFilterer mFilterer;

        public TestListRunner(TestRunnerStorage storage,
            TestListFilterer filterer)
        {
            mStorage = storage;
            mFilterer = filterer;
        }

        public void Run()
        {
            mStorage.Foreach(
                (assemblyName, fixtureType, testFunction, categoryName) =>
                {
                    if(mFilterer == null || mFilterer.Check(assemblyName, fixtureType.FullName,
                        testFunction.Name, categoryName))
                    {
                        try
                        {
                            var testFixture = Activator.CreateInstance(fixtureType);
                            var testRun = new SingleTestRunner(testFixture, testFunction);
                            testRun.Run();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                );
        }
    }

}
