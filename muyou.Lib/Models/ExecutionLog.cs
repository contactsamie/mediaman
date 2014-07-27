using System;

namespace muyou.Lib
{
    public class ExecutionLog
    {
        public ExecutionLog(string name, string body)
        {
            Name = name;
            Body = body;
        }

        public UInt64 NumberOfExceptions = 0;

        public string Name { set; get; }

        public string Body { set; get; }

        public Exception ExceptionThrown { set; get; }

        public DateTime ExecutionStartTime { set; get; }

        public DateTime ExecutionEndTime { set; get; }

        public UInt64 Position { set; get; }

        public object ExecutionResult { set; get; }

        public void SetThrownException(Exception e)
        {
            NumberOfExceptions++;
            ExceptionThrown = e;
        }

        public void ExecutionStarted(UInt64 position)
        {
            Position = position;
            ExecutionStartTime = DateTime.Now;
        }

        public void ExecutionEnded()
        {
            ExecutionEndTime = DateTime.Now;
        }
    }
}