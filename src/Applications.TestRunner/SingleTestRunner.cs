using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Afterglow.Applications.TestRunner
{
    public class SingleTestRunner
    {
        private object mTestFixture;

        private MethodInfo mTestMethodInfo;

        public SingleTestRunner(object testFixture, MethodInfo testMethodInfo)
        {
            mTestFixture = testFixture;
            mTestMethodInfo = testMethodInfo;
        }

        public void Run()
        {
            Console.WriteLine("Starting Test Run: {0}.{1}",
                mTestFixture.GetType().FullName, mTestMethodInfo.Name);

            try
            {
                mTestMethodInfo.Invoke(mTestFixture, null);
            }
            catch (Exception exception)
            {
                Console.Write(exception.ToString());
            }


            Console.WriteLine("Finishing Test Run: {0}.{1}",
                mTestFixture.GetType().FullName, mTestMethodInfo.Name);
        }
    }

}
