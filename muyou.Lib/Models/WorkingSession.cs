using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace muyou.Lib
{
    public class WorkingSession
    {
        public WorkingSession()
        {
            SessionId = Guid.NewGuid();
            DownloadList = new List<Downloads>();
        }

        public Guid? SessionId { set; get; }

        public List<Downloads> DownloadList { set; get; }

        public DateTime LastSaved { set; get; }

        public DateTime Created { set; get; }

        public DateTime LastLoaded { set; get; }

        public int Version { set; get; }

        public SessionFileState SessionFileState { set; get; }
        [XmlIgnore]
        public Exception StateReason { set; get; }
    }
}