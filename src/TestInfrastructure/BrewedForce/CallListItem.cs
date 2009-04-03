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
        #region public interface ######################################################################

        public CallListItem(Type callingType, object callingInstance, MethodInfo callingMethod)
            : this(callingType, callingInstance, callingMethod, new object[0])
        {
            ;
        }

        public CallListItem(Type callingType, object callingInstance, MethodInfo callingMethod, object[] parameterValues)
            : this(callingType, callingInstance, callingMethod, parameterValues, null)
        {
            ;
        }

        public CallListItem(Type callingType, object callingInstance, MethodInfo callingMethod, Exception exception)
            : this(callingType, callingInstance, callingMethod, new object[0], exception)
        {
            ;
        }

        public CallListItem(Type callingType, object callingInstance, MethodInfo callingMethod,
            object[] parameterValues, Exception exception)
        {
            mException = exception;
            mCallingType = callingType;
            mCallingInstance = callingInstance;
            mCallingMethod = callingMethod;
            mParameterValues = parameterValues;
        }

        public static string GetInstanceTextInfo(object instance)
        {
            if (instance == null)
                return "[null:NoneType]";
            return string.Format("[{0}:{1}]", instance.GetHashCode(), instance.GetType().Name);
        }

        public static string GetParameterTextInfo(object instance, ParameterInfo parameterInfo)
        {
            if (instance == null || instance.GetType().Equals(parameterInfo.ParameterType))
                return GetInstanceTextInfo(instance);

            return string.Format("[{0}:{1}>{2}]", instance.GetHashCode(), instance.GetType(), parameterInfo.ParameterType.Name);
        }

        public static string GetCallTextInfo(object instance, MethodInfo methodInfo,
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

            return text;
        }

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


        public object[] ParameterValues
        {
            get { return mParameterValues; }
        }

        public Exception Exception
        {
            get { return mException; }
        }

        public Type CallingType
        {
            get { return mCallingType; }
        }

        public object CallingInstance
        {
            get { return mCallingInstance; }
        }

        public MethodInfo CallingMethod
        {
            get { return mCallingMethod; }
        }

        public const char LINE_SEPARATOR = '\n';
        public const string INDENTION = "  ";

        #endregion public interface

        #region private state attributes ##############################################################    
        
        private readonly Exception mException;

        private readonly Type mCallingType;

        private readonly object mCallingInstance;

        private readonly MethodInfo mCallingMethod;

        private readonly object[] mParameterValues;

        #endregion private state attributes
    }

}
