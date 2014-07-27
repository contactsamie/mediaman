using muyou.Lib.Downloaders;
using System;
using System.Collections.Generic;

namespace muyou.Lib
{
    public class DownloadChain
    {

        private WorkingSessionManager SessionManager { set; get; }

        public DownloadChain(WorkingSessionManager sessionManager)
        {
            SessionManager = sessionManager;
            if (sessionManager == null) throw new ArgumentNullException("sessionManager");
        }

        public List<Exception> ExecuteAllDownloads( Action<Exception, string> ended=null, Action<int, string> progress=null)
        {
            var exceptions = new List<Exception>();

            Load().ForEach(downloader =>
            {
                downloader.ProcessDownload(SessionManager, ended, progress);

                if (downloader.ProgressIndicatorExceptions != null && downloader.ProgressIndicatorExceptions.Count > 0)
                {
                    exceptions.AddRange(downloader.ProgressIndicatorExceptions);
                }
            });
            SessionManager.SaveSession();
            return exceptions;
        }
        private static List<AbstractDownloadExecutor> Load()
        {
            var chainOfExecution = new List<AbstractDownloadExecutor>
            {
                new FileDownloadExecutor()
            };

            return chainOfExecution;
        }

       
    }
}