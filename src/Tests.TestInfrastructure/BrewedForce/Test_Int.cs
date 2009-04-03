using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Afterglow.Infrastructure;
using Afterglow.Infrastructure.BrewedForce;
using MbUnit.Framework;

namespace TheNewEngine.Infrastructure.BrewedForce
{
    public class Test_Int : Test_Class<int>
    {
        public Test_Int()
        {
            //Define some default parameters to let the tests pass...
            this.AddDefault_Parse_DefaultArgumentsTo();
        }

        [Test]
        public void Call_everything_and_catch_only_precondition_exceptions()
        {
            base.CallEverything();
        }

    }
}
