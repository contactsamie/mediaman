using System;
using System.Net;

namespace muyou.Lib.Downloaders
{
    public class FileDownloadExecutor : AbstractDownloadExecutor
    {

        protected override void ProcessDownloadList(string downloadUrl, string localFile, Action<int, string> progress)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(downloadUrl, localFile);
                progress.Invoke(100, "saving download");
            }
        }
    }
}