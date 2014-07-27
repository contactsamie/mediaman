using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace muyou.Lib
{
    public static class Utility
    {
        public static UInt64 Position = 0;
        public static List<ExecutionLog> LogExecution = new List<ExecutionLog>();
        public static void Serialize(object obj, string file)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var writer = new StreamWriter(file);
            serializer.Serialize(writer.BaseStream, obj);
            writer.Close();
            writer.Dispose();
        }

        public static T DeSerialize<T>(string file)
        {

            using (Stream loadstream = new FileStream(file, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(T));
               return (T)serializer.Deserialize(loadstream);

            }
          
        }
        public static TReturnType ExecuteAndHandleExceptions<TReturnType>(Func<TReturnType> executionContext, bool debug = true)
        {
            TReturnType result;
            Exception ex = null;
            var methodInfo = executionContext.GetMethodInfo();
            var methodBody = methodInfo.GetMethodBody();
            var executionLog = new ExecutionLog(methodInfo.Name, methodBody == null ? "" : methodBody.ToString());
            Position++;
            executionLog.ExecutionStarted(Position);
            try
            {
                result = executionContext.Invoke();
            }
            catch (Exception e)
            {
                ex = e;
                result = default(TReturnType);
                executionLog.SetThrownException(e);
            }
            executionLog.ExecutionEnded();
            executionLog.ExecutionResult = result;

            LogExecution.Add(executionLog);

            if (debug && ex != null) throw ex;
            return result;
        }
    }
}