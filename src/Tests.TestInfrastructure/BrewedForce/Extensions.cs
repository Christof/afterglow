using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Afterglow.Infrastructure.BrewedForce;

namespace Afterglow.Infrastructure.BrewedForce
{
    public static class DefaultParametersExtensions
    {
        public static void AddDefault_Parse_DefaultArgumentsTo<T>(this Test_Class<T> test)
        {
            AddDefault_Parse_DefaultArgumentsTo(test, typeof(T) );
        }


        public static void AddDefault_Parse_DefaultArgumentsTo<T>(this Test_Class<T> test, Type type)
        {
            var parse1 = type.GetMethod("Parse", new[] { typeof(string) });
            {
                var parameter1 = parse1.GetParameters()[0];
                test.DefaultParameterValues.Add(parameter1, () => "42");
            }

            var parse2 = type.GetMethod("Parse", new[] { typeof(string), typeof(NumberStyles) });
            {
                var parameter1 = parse2.GetParameters()[0];
                test.DefaultParameterValues.Add(parameter1, () => "42");

                var parameter2 = parse2.GetParameters()[1];
                test.DefaultParameterValues.Add(parameter2, () => NumberStyles.Number);
            }

            var parse3 = type.GetMethod("Parse", new[] { typeof(string), typeof(IFormatProvider) });
            {
                var parameter1 = parse3.GetParameters()[0];
                test.DefaultParameterValues.Add(parameter1, () => "42");

                var parameter2 = parse3.GetParameters()[1];
                test.DefaultParameterValues.Add(parameter2, () => NumberFormatInfo.CurrentInfo);
            }

            var parse4 = type.GetMethod("Parse", new[] { typeof(string), typeof(NumberStyles), typeof(IFormatProvider) });
            {
                var parameter1 = parse4.GetParameters()[0];
                test.DefaultParameterValues.Add(parameter1, () => "42");

                var parameter2 = parse4.GetParameters()[1];
                test.DefaultParameterValues.Add(parameter2, () => NumberStyles.Number);

                var parameter3 = parse4.GetParameters()[2];
                test.DefaultParameterValues.Add(parameter3, () => NumberFormatInfo.CurrentInfo);
            }
        }
    }
}
