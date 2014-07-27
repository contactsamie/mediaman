using System;
using System.IO;

namespace muyou.Lib
{

    public class DownloadedFileInformation
    {
        public long size { set; get; }
        public string sizeUnit { set; get; }
    }
    public class Downloads
    {
      

        public DownLoadState DownLoadState { set; get; }

        public string DownloadStatus { set; get; }
        public Guid? DownloadId { set; get; }
        public string DownloadUrl { set; get; }
        public string NameOfDownload { set; get; }
        public string Description { set; get; }
        public string ImageUrl { set; get; }

        public DownloadedFileInformation FileInformation =new DownloadedFileInformation();

        public ResourceType ResourceType { set; get; }
        public string LocalPath { set; get; }
        public string LocalFileName { set; get; }
        public string LocalExtension { set; get; }
        public string FinalLocalPath { set; get; }


        public FileInfo TryUpdateFileInfo()
        {
            try
            {
             var fi=     new FileInfo(BuildLocalDownloadFileName());


             FileInformation.size = fi.Length ;

             FileInformation.sizeUnit = "B";

             if (FileInformation.size > 1024)
             {

                 FileInformation.size = FileInformation.size / 1024;

                FileInformation.sizeUnit = "KB";

                if (FileInformation.size > 1024)
                {

                    FileInformation.size = FileInformation.size / 1024;

                    FileInformation.sizeUnit = "MB";

                }

             }





              

                return fi;
            }
            catch (Exception)
            {
                
            
            }

            return null;

        }

        public string BuildLocalDownloadFileName()
        {
           
            return LocalPath + "\\" + LocalFileName + "." + LocalExtension;

        }
    }
}