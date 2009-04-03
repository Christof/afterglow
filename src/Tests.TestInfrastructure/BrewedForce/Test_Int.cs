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

namespace TheNewEngine.Infrastructure
{
    public class Test_Int : Test_Class<int>
    {
        public Test_Int()
        {
            //Define some default parameters to let the tests pass...

            var parse1 = typeof(int).GetMethod("Parse", new[] { typeof(string) });
            {
                var parameter1 = parse1.GetParameters()[0];
                base.DefaultParameterValues.Add(parameter1, () => "42");
            }
 
            var parse2 = typeof(int).GetMethod("Parse", new[] { typeof(string), typeof(NumberStyles)});
            {
                var parameter1 = parse2.GetParameters()[0];
                base.DefaultParameterValues.Add(parameter1, () => "42");

                var parameter2 = parse2.GetParameters()[1];
                base.DefaultParameterValues.Add(parameter2, () => NumberStyles.Number);
            }

            var parse3 = typeof(int).GetMethod("Parse", new[] { typeof(string), typeof(IFormatProvider) });
            {
                var parameter1 = parse3.GetParameters()[0];
                base.DefaultParameterValues.Add(parameter1, () => "42");

                var parameter2 = parse3.GetParameters()[1];
                base.DefaultParameterValues.Add(parameter2, () => NumberFormatInfo.CurrentInfo);
            }

            var parse4 = typeof(int).GetMethod("Parse", new[] { typeof(string), typeof(NumberStyles), typeof(IFormatProvider)});
            {
                var parameter1 = parse4.GetParameters()[0];
                base.DefaultParameterValues.Add(parameter1, () => "42");

                var parameter2 = parse4.GetParameters()[1];
                base.DefaultParameterValues.Add(parameter2, () => NumberStyles.Number);

                var parameter3 = parse4.GetParameters()[2];
                base.DefaultParameterValues.Add(parameter3, () => NumberFormatInfo.CurrentInfo);
            }
            
        }

        [Test]
        public void Call_everything_and_catch_only_precondition_exceptions()
        {
            base.Call_everything_and_catch_only_precondition_exceptions();
        }

    }
}
