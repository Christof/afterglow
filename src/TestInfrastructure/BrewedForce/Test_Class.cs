using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Gallio.Model;
using MbUnit.Framework;
using Microsoft.Contracts;
using TheNewEngine.Infrastructure;
using TheNewEngine.Infrastructure.BrewedForce;

namespace Afterglow.Infrastructure.BrewedForce
{
    /// <summary>
    /// Tries all callable methods, properties, constructors of a given class.
    /// </summary>
    [TestFixture]
    public class Test_Class<T>
        where T : new()
    {
        #region public interface ######################################################################
        
        public List<CallListItem> CallList
        {
            get; private set;
        }

        public List<Type> CatchedExceptionTypes
        {
            get;
            private set;
        }

        public Func<object> CreateInstance
        {
            get;
            set;
        }

        public Dictionary<ParameterInfo, Func<object>> DefaultParameterValues
        {
            get; set;
        }

        public List<CallListItem> FailedCalls
        {
            get
            {
                var errorList = new List<CallListItem>();
                this.CallList.ForEach(item =>
                                      {
                                          if (item.Exception != null)
                                          {
                                              errorList.Add(item);
                                          }
                                      });
                return errorList;
            }
        }

        public string FailedCallsTextInfo
        {
            get
            {
                var errorMessage = string.Empty;
                this.FailedCalls.ForEach(item => errorMessage+= item.ToString() + CallListItem.LINE_SEPARATOR);
                return errorMessage;
            }
        }
        public List<CallListItem> PassedCalls
        {
            get
            {
                var passedList = new List<CallListItem>();
                this.CallList.ForEach(item =>
                {
                    if (item.Exception == null)
                    {
                        passedList.Add(item);
                    }
                });
                return passedList;
            }
        }


        public string PassedCallsTextInfo
        {
            get
            {
                var callTextInfo = string.Empty;
                this.PassedCalls.ForEach(item => callTextInfo += item.ToString() + CallListItem.LINE_SEPARATOR);
                return callTextInfo;
            }
        }
        
        [Test]
        public void Call_everything_and_catch_only_precondition_exceptions()
        {
            this.CallList.Clear();
            var type = this.CreateInstance().GetType();

            foreach (MemberInfo memberInfo in type.GetMembers())
            {
                if(!memberInfo.DeclaringType.Equals(type))
                    continue;

                object[] ignores = memberInfo.GetCustomAttributes(typeof (IgnoreAttribute), false);
                if (ignores.Length > 0)
                {
                    Trace.WriteLine("Ignoring Class Member: " + memberInfo.Name, "Warning");
                    continue;
                }
                object[] testAttributes = memberInfo.GetCustomAttributes(typeof (TestAttribute), false);
                if (testAttributes.Length > 0)
                {
                    continue;
                }

                switch (memberInfo.MemberType)
                {
                    case (MemberTypes.Method):
                        Test_call_method_with_auto_parameters_and_catch_only_precondition_exceptions(memberInfo);
                        break;
                    case (MemberTypes.Property):
                        //TODO: call getter, setter
                        break;
                    default:
                        continue;
                }
            }
            Trace.WriteLine(string.Format("______________PASSED CALLS __{0}/{1}________________", this.PassedCalls.Count, this.CallList.Count));
            Trace.Write(this.PassedCallsTextInfo);
            if (FailedCalls.Count != 0)
            {
                Trace.WriteLine(string.Format("______________FAILED CALLS __{0}/{1}________________", this.FailedCalls.Count, this.CallList.Count));
                Trace.Write(this.FailedCallsTextInfo);
                Assert.TerminateSilently(TestOutcome.Failed, "Failed cause of the above error(s)");
            }
        }

        public Test_Class()
        {
            this.CallList = new List<CallListItem>();
            this.CatchedExceptionTypes = new List<Type>
            {
                typeof (PreconditionException),
                typeof (ArgumentException),
                typeof (ArgumentNullException),
                typeof (ArgumentOutOfRangeException)
            };

            this.DefaultParameterValues = new Dictionary<ParameterInfo, Func<object>>();
            this.CreateInstance = () => typeof (T).TryCreateInstance();
        }

        #endregion public interface

        #region private helper ######################################################################

        private const string BEGIN_TEXT = "#region ";
        private const string END_TEXT = "#endregion ";

        private void Test_call_method_with_auto_parameters_and_catch_only_precondition_exceptions(MemberInfo memberInfo)
        {
            var methodInfo = memberInfo as MethodInfo;
            if (methodInfo == null ||
                !methodInfo.IsPublic ||
                    methodInfo.IsGenericMethodDefinition ||
                        methodInfo.IsAbstract)
            {
                return;
            }

            ParameterInfo[] parameterInfos = methodInfo.GetParameters();
            var parameterValues = new List<object>();
            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                object value = null;
                if(this.DefaultParameterValues.ContainsKey(parameterInfo))
                {
                    value = this.DefaultParameterValues[parameterInfo]();
                }
                else
                {
                    Type parameterType = parameterInfo.ParameterType;
                    value = parameterType.TryCreateInstance();
                }

                parameterValues.Add(value);
            }

            Test_call_method_and_catch_only_precondition_exceptions(
                methodInfo,
                parameterValues.ToArray());
        }


        private void Test_call_method_and_catch_only_precondition_exceptions(
            MethodInfo methodInfo,
            object[] parameterValues)
        {
            var instance = CreateInstance();

            var callListItem = new CallListItem(methodInfo.GetType(), instance, methodInfo, parameterValues);
            Trace.Write(BEGIN_TEXT);
            Trace.WriteLine(callListItem.ToString());

            try
            {
                methodInfo.Invoke(instance, parameterValues);
            }
            catch (TargetInvocationException ex)
            {
                callListItem = new CallListItem(methodInfo.GetType(), instance, methodInfo, ex.InnerException);
            }

            Trace.Write(END_TEXT);
            Trace.WriteLine(callListItem.ToString());
            Trace.WriteLine(string.Empty);

            this.CallList.Add(callListItem);
        }

        
        #endregion private helper
    }
}