using System;
using System.Collections.Generic;

namespace muyou.Lib.Downloaders
{
    public abstract class AbstractDownloadExecutor
    {
        protected abstract void ProcessDownloadList(string downloadUrl, string localFile, Action<int, string> progress);

        public List<Exception> ProgressIndicatorExceptions = new List<Exception>();

        public void ProcessDownload(WorkingSessionManager currentSessionManager, Action<Exception, string> ended = null, Action<int, string> progress = null)
        {
            var currentSession = currentSessionManager.WorkingSession;

            if (currentSession == null) throw new ArgumentNullException("currentSession");

            if (currentSession.DownloadList == null) throw new ArgumentNullException("downloadList");
            progress = progress ?? ((level, message) => { });

            ended = ended ?? ((exception, message) => { });
            try
            {
                progress.Invoke(100, "Download started");

                currentSession.DownloadList.FindAll(x => x.DownLoadState == DownLoadState.UNKNOWN).ForEach(download =>
                {
                    download.DownLoadState = DownLoadState.NOT_YET_DOWNLOADED;
                    try
                    {
                        ProcessDownloadList(download.DownloadUrl, download.BuildLocalDownloadFileName(), delegate(int level, String message)
                        {
                            try
                            {
                                progress.Invoke(level, message);
                            }
                            catch (Exception e)
                            {
                                ProgressIndicatorExceptions.Add(e);
                            }
                        });
                        download.DownLoadState = DownLoadState.DOWNLOADED;
                    }
                    catch (Exception)
                    {
                        progress.Invoke(100, "unable to download file");
                        download.DownLoadState = DownLoadState.UNABLE_TO_DOWNLOAD;
                    }
                });
                progress.Invoke(100, "Download complete");
            }
            catch (Exception e)
            {
                try
                {
                    ended.Invoke(e, "Download Failed");
                }
                catch (Exception ex)
                {
                    ProgressIndicatorExceptions.Add(ex);
                }
            }
            currentSession.DownloadList.ForEach(x =>
            {
                var file = x.BuildLocalDownloadFileName();

                if (currentSessionManager.DirectorySanitizer.FileManager.Exists(file))
                {
                    x.DownLoadState = DownLoadState.VERIFIED_DOWNLOADED;
                }
            });
            ended.Invoke(null, "Download completed successfully");
        }
    }
}