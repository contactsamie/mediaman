using System;

namespace muyou.Lib
{
    public class Downloads
    {

        public DownLoadState DownLoadState { set; get; }

        public Guid? DownloadId { set; get; }
        public string DownloadUrl { set; get; }
        public string NameOfDownload { set; get; }
        public string Description { set; get; }
        public string ImageUrl { set; get; }

        public ResourceType ResourceType { set; get; }
        public string LocalPath { set; get; }
        public string LocalFileName { set; get; }
        public string LocalExtension { set; get; }
        public string FinalLocalPath { set; get; }

        public string BuildLocalDownloadFileName()
        {
            return LocalPath + "\\" + LocalFileName + "." + LocalExtension;
        }
    }
}