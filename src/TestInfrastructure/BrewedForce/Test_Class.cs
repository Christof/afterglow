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
    {
        #region public interface ######################################################################
        
        /// <summary>
        /// list of calls which have been done
        /// </summary>
        public List<CallListItem> CallList
        {
            get; private set;
        }

        /// <summary>
        /// exception types which are allowed
        /// </summary>
        public List<Type> CatchedExceptionTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// function which is used for creating an instance of the object to test
        /// </summary>
        public Func<object> CreateInstance
        {
            get;
            set;
        }

        /// <summary>
        /// parameter values which are used for the specific parameter
        /// </summary>
        public Dictionary<ParameterInfo, Func<object>> DefaultParameterValues
        {
            get; set;
        }

        /// <summary>
        /// calls which failed
        /// </summary>
        public List<CallListItem> FailedCalls
        {
            get
            {
                var errorList = new List<CallListItem>();
                this.CallList.ForEach(item =>
                                      {
                                          if (item.Exception != null && !this.CatchedExceptionTypes.Contains(item.Exception.GetType()) )
                                          {
                                              errorList.Add(item);
                                          }
                                      });
                return errorList;
            }
        }

        /// <summary>
        /// text about the failed calls
        /// </summary>
        public string FailedCallsTextInfo
        {
            get
            {
                return FormatCalls(this.FailedCalls);
            }
        }

        /// <summary>
        /// list of passed calls
        /// </summary>
        public List<CallListItem> PassedCalls
        {
            get
            {
                var passedList = new List<CallListItem>();
                this.CallList.ForEach(item =>
                {
                    if (item.Exception == null || this.CatchedExceptionTypes.Contains(item.Exception.GetType()) )
                    {
                        passedList.Add(item);
                    }
                });
                return passedList;
            }
        }

        /// <summary>
        /// text about the passed calls
        /// </summary>
        public string PassedCallsTextInfo
        {
            get
            {
                return FormatCalls(this.PassedCalls);
            }
        }
        
        /// <summary>
        /// calls everything from the instance which is any kind of callable
        /// </summary>
        [Test]
        public void CallEverything()
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

                this.CallMember(memberInfo);
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

        /// <summary>
        /// Constructor, which inits the attributes with default stuff
        /// </summary>
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

        /// <summary>
        /// text which is used for marking the beginning of a call in the log
        /// </summary>
        private const string BEGIN_TEXT = "#region ";

        /// <summary>
        /// text which is used for marking the ending of a call in the log
        /// </summary>
        private const string END_TEXT = "#endregion ";

        /// <summary>
        /// formats a list of calls
        /// </summary>
        /// <param name="callList">list of calls to format</param>
        /// <returns>text about the list of calls</returns>
        private static string FormatCalls(IEnumerable<CallListItem> callList)
        {
            var callTextInfo = string.Empty;
            callList.ForEach(item => callTextInfo += item.ToString() + CallListItem.LINE_SEPARATOR);
            return callTextInfo;
        }

        /// <summary>
        /// calls a member, if it makes sense, and catches only defined exception types
        /// </summary>
        /// <param name="memberInfo">member to call</param>
        private void CallMember(MemberInfo memberInfo)
        {
            switch (memberInfo.MemberType)
            {
                case (MemberTypes.Method):
                case (MemberTypes.Constructor):
                    CallMethodAfterGeneratingParameterValues(memberInfo as MethodBase);
                    break;
                case (MemberTypes.Property):
                    CallProperty(memberInfo as PropertyInfo);
                    break;

            }
        }

        /// <summary>
        /// calls a properties getter and setter if possible
        /// </summary>
        /// <param name="propertyInfo">the property to call</param>
        private void CallProperty(PropertyInfo propertyInfo)
        {
            if(propertyInfo.CanRead)
                this.CallMethodAfterGeneratingParameterValues(propertyInfo.GetGetMethod(false));
            if (propertyInfo.CanWrite)
                this.CallMethodAfterGeneratingParameterValues(propertyInfo.GetSetMethod(false));
        }
        
        /// <summary>
        /// calls a method with auto generated parameters
        /// </summary>
        /// <param name="methodInfo"></param>
        private void CallMethodAfterGeneratingParameterValues(
            MethodBase methodInfo)
        {
            if (!methodInfo.IsPublic ||
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
                if (this.DefaultParameterValues.ContainsKey(parameterInfo))
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

            this.CallMethod(methodInfo, parameterValues.ToArray() );
        }


        /// <summary>
        /// calls a method with auto generated parameters
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="parameterValues"></param>
        private void CallMethod(
            MethodBase methodInfo,
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
            catch(NotSupportedException ex)
            {
                //in some cases this type is thrown... security?
                //found occurancies: [String].Concat([42:System.Int32>Object], [42:System.Int32>Object], [42:System.Int32>Object], [42:System.Int32>Object])
                callListItem = new CallListItem(methodInfo.GetType(), instance, methodInfo, ex);
            }

            Trace.Write(END_TEXT);
            Trace.WriteLine(callListItem.ToString());
            Trace.WriteLine(string.Empty);

            this.CallList.Add(callListItem);
        }
        
        #endregion private helper
    }
}