using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TheNewEngine.Infrastructure.BrewedForce
{
    /// <summary>
    /// A single item in the call list of Test_Class.
    /// Holds information about the type, the instance, optional parameters, an optional exception
    /// and the tested method itself.
    /// </summary>
    /// <remarks>
    /// Implements the Immutable pattern.
    /// </remarks>
    public class CallListItem
    {
        #region constructors ##########################################################################

        /// <summary>
        /// Constructs CallListItem
        /// </summary>
        /// <param name="callingType">type of the object</param>
        /// <param name="callingInstance">the instance which is used for calling the method</param>
        /// <param name="callingMethod">the method which is called</param>
        /// <param name="parameterValues">optional parameter values which are passed to the method which is called</param>
        /// <param name="exception">optional exception which has been thrown by calling the method</param>
        public CallListItem(Type callingType, object callingInstance, MethodBase callingMethod,
            object[] parameterValues, Exception exception)
        {
            mException = exception;
            mCallingType = callingType;
            mCallingInstance = callingInstance;
            mCallingMethod = callingMethod;
            mParameterValues = parameterValues;
        }

        /// <summary>
        /// Constructs CallListItem without parameters
        /// </summary>
        /// <param name="callingType">type of the object</param>
        /// <param name="callingInstance">the instance which is used for calling the method</param>
        /// <param name="callingMethod">the method which is called</param>
        public CallListItem(Type callingType, object callingInstance, MethodBase callingMethod)
            : this(callingType, callingInstance, callingMethod, new object[0])
        {
            ;
        }

        /// <summary>
        /// Constructs CallListItem without an exception which has been thrown by calling the method
        /// </summary>
        /// <param name="callingType">type of the object</param>
        /// <param name="callingInstance">the instance which is used for calling the method</param>
        /// <param name="callingMethod">the method which is called</param>
        /// <param name="parameterValues">optional parameter values which are passed to the method which is called</param>
        public CallListItem(Type callingType, object callingInstance, MethodBase callingMethod, object[] parameterValues)
            : this(callingType, callingInstance, callingMethod, parameterValues, null)
        {
            ;
        }

        /// <summary>
        /// Constructs CallListItem without parameters, but with an exception 
        /// </summary>
        /// <param name="callingType">type of the object</param>
        /// <param name="callingInstance">the instance which is used for calling the method</param>
        /// <param name="callingMethod">the method which is called</param>
        /// <param name="exception">optional exception which has been thrown by calling the method</param>
        public CallListItem(Type callingType, object callingInstance, MethodBase callingMethod, Exception exception)
            : this(callingType, callingInstance, callingMethod, new object[0], exception)
        {
            ;
        }

        #endregion constructors

        #region public interface ######################################################################

        /// <summary>
        /// Formats the log of an instance
        /// </summary>
        /// <param name="instance">the instance from which information is logged</param>
        /// <returns>a string representing a title of the instance</returns>
        public static string GetInstanceTextInfo(object instance)
        {
            if (instance == null)
                return "[null:NoneType]";
            return string.Format("[{0}:{1}]", instance.GetHashCode(), instance.GetType().Name);
        }

        /// <summary>
        /// Formats an instance which is a parameter for a method
        /// </summary>
        /// <param name="instance">the instance from which information is formated</param>
        /// <param name="parameterInfo">information about the parameter</param>
        /// <returns>a string representing a title of the parameter</returns>
        public static string GetParameterTextInfo(object instance, ParameterInfo parameterInfo)
        {
            if (instance == null || instance.GetType().Equals(parameterInfo.ParameterType))
                return GetInstanceTextInfo(instance);

            return string.Format("[{0}:{1}>{2}]", instance.GetHashCode(), instance.GetType(), parameterInfo.ParameterType.Name);
        }

        /// <summary>
        /// Formats information about a method call
        /// </summary>
        /// <param name="instance">instance which is used for calling the method</param>
        /// <param name="methodInfo">method which is called</param>
        /// <param name="parameterValues">parameter values which are passed to the method when calling</param>
        /// <returns>string representing a short text about the method call</returns>
        public static string GetCallTextInfo(object instance, MethodBase methodInfo,
            object[] parameterValues)
        {
            var parameterInfos = string.Empty;
            for (int i = 0; i < parameterValues.Length; i++)
            {
                var parameterInfo = methodInfo.GetParameters()[i];
                parameterInfos += GetParameterTextInfo(parameterValues[i], parameterInfo);
                if (i < parameterValues.Length - 1)
                    parameterInfos += ", ";
            }

            return string.Format(
                    ".{0}({1})",
                    methodInfo.Name,
                    parameterInfos);
        }

        /// <summary>
        /// Transforms the call list item to a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var text = String.Empty;

            var instanceInfo = this.CallingMethod.IsStatic
                ?
                    string.Format("[{0}]", this.CallingInstance.GetType().Name)
                :
                    GetInstanceTextInfo(this.CallingInstance);

            var callInfo = GetCallTextInfo(
                this.CallingInstance,
                this.CallingMethod, this.ParameterValues);

            text += instanceInfo;
            text += callInfo;

            text += this.ExceptionTextInfo;

            return text;
        }

        /// <summary>
        /// Text about the exception if there was one
        /// </summary>
        public string ExceptionTextInfo
        {
            get
            {
                var text = String.Empty;
                if (this.Exception != null)
                {
                    text += INDENTION + this.Exception.GetType().Name + LINE_SEPARATOR;
                    text += INDENTION + this.Exception.Message + LINE_SEPARATOR;

                    var stackTrace = this.Exception.StackTrace;
                    stackTrace = stackTrace.Replace(LINE_SEPARATOR + string.Empty, LINE_SEPARATOR + INDENTION);
                    text += INDENTION + stackTrace + LINE_SEPARATOR;
                }
                return text;
            }
        }

        /// <summary>
        /// the parameters which are used for calling the method
        /// </summary>
        public object[] ParameterValues
        {
            get { return mParameterValues; }
        }

        /// <summary>
        /// Optional Exception which could have been thrown by calling the method
        /// </summary>
        public Exception Exception
        {
            get { return mException; }
        }

        /// <summary>
        /// Type of the instance which is used for calling the method
        /// </summary>
        public Type CallingType
        {
            get { return mCallingType; }
        }

        /// <summary>
        /// Instance for which the method is called
        /// </summary>
        public object CallingInstance
        {
            get { return mCallingInstance; }
        }

        /// <summary>
        /// method which is called
        /// </summary>
        public MethodBase CallingMethod
        {
            get { return mCallingMethod; }
        }

        /// <summary>
        /// character which is used for line separation
        /// </summary>
        public const char LINE_SEPARATOR = '\n';

        /// <summary>
        /// character which is used for idention
        /// </summary>
        public const string INDENTION = "  ";

        #endregion public interface

        #region private state attributes ##############################################################    
        
        /// <summary>
        /// Exception which could have been thrown by calling the method
        /// </summary>
        private readonly Exception mException;

        /// <summary>
        /// type of the object which is called
        /// </summary>
        private readonly Type mCallingType;

        /// <summary>
        /// instance on which the method is called
        /// </summary>
        private readonly object mCallingInstance;

        /// <summary>
        /// method which is called
        /// </summary>
        private readonly MethodBase mCallingMethod;

        /// <summary>
        /// Parameters which are passed to the method
        /// </summary>
        private readonly object[] mParameterValues;

        #endregion private state attributes
    }

}
