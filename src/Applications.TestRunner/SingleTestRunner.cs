using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Trace.WriteLine(string.Format("#Begin Test Run: {0}.{1}",
                mTestFixture.GetType().FullName, mTestMethodInfo.Name));

            try
            {
                mTestMethodInfo.Invoke(mTestFixture, new object[0] );
            }
            catch (Exception exception)
            {
                Trace.WriteLine("ERROR:");
                Trace.WriteLine(exception.Message);

                var innerException = exception.InnerException;
                if (innerException != null)
                {
                    Trace.WriteLine(innerException.GetType().Name);
                    Trace.WriteLine(innerException.Message);
                }
                Trace.WriteLine(string.Empty);
            }


            Trace.WriteLine(string.Format("#End Test Run: {0}.{1}",
                mTestFixture.GetType().FullName, mTestMethodInfo.Name));
        }
    }

}
