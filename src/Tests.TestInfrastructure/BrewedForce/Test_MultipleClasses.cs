using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Afterglow.Infrastructure;
using Afterglow.Infrastructure.BrewedForce;
using MbUnit.Framework;

namespace TheNewEngine.Infrastructure.BrewedForce
{
    public class Test_MultipleClasses : Test_Class<object>
    {
        public Test_MultipleClasses()
        {

            //Define some default parameters to let the tests pass...
            this.AddDefault_Parse_DefaultArgumentsTo(typeof(Double));
            this.AddDefault_Parse_DefaultArgumentsTo(typeof(Single));
        }

        [Test]
        public void Test_some_classes()
        {
            var types = new[]
            {
                typeof (String),
                typeof (Double),
                typeof (Single)
            };

            //get, set chars throw wrong exception, so...
            base.CatchedExceptionTypes.Add(typeof(IndexOutOfRangeException));

            //string.concat() without arguments is defined but...
            base.CatchedExceptionTypes.Add(typeof(NotSupportedException));

            types.ForEach(
                type =>
                {
                    base.CreateInstance = () => type.TryCreateInstance();
                    base.CallEverything();
                });
    }
    }
}
