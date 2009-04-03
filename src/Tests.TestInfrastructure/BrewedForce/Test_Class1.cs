using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Afterglow.Infrastructure;
using Afterglow.Infrastructure.BrewedForce;
using MbUnit.Framework;

namespace TheNewEngine.Infrastructure
{
    public class Class1
    {
        public Class1()
        {
            ;
        }

        public void Method1()
        {
            Trace.WriteLine("Hello World!");
        }
    }

    public class Test_Class1 : Test_Class<Class1>
    {
        [Test]
        public void Test_call_everything_and_catch_only_precondition_exceptions()
        {
            base.Call_everything_and_catch_only_precondition_exceptions();
        }
    }
}
