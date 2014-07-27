using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace muyou.Lib
{
    public class WorkingSessionManager
    {
      

        public DirectorySanitizer DirectorySanitizer { set; get; }

        public WorkingSessionManager(DirectorySanitizer directorySanitizer)
        {
            if (directorySanitizer == null) throw new ArgumentNullException("directorySanitizer");
            if (string.IsNullOrEmpty(directorySanitizer.SessionFile)) throw new ArgumentNullException("SessionFile");
            DirectorySanitizer = directorySanitizer;
        }

        public Downloads CreateNewDownload(string DownloadUrl, ResourceType ResourceType=ResourceType.UNKNOWN,string LocalExtension=null, string ImageUrl="",string NameOfDownload="",string Description = "")
        {


            var x = DownloadUrl.Split('/');
            var filename = x[x.Length - 1].Replace('.', '_').Replace(',', '_');
            var y = x[x.Length - 1].Split('.');

            var ext = y[y.Length - 1];
            var id = Guid.NewGuid();
            var download= new Downloads()
            {
                Description = Description,
                DownloadId = id,
                DownLoadState = DownLoadState.UNKNOWN,
                ImageUrl = ImageUrl,
                LocalExtension = ext,
                LocalFileName = filename,
                LocalPath = DirectorySanitizer.SessionFolder+"\\"+id,
                NameOfDownload = NameOfDownload,
                ResourceType = ResourceType,
                DownloadUrl = DownloadUrl,
                FinalLocalPath = ComputeFinalLocalPath(ResourceType)
            };
            DirectorySanitizer.FolderManager.ReplaceEveIfInExistence(download.LocalPath,"");
            return download;

        }

        private string ComputeFinalLocalPath(ResourceType resourceType)
        {
            if (resourceType == ResourceType.IMAGE)
                return DirectorySanitizer.VideoFolder;


            if (resourceType == ResourceType.YOUTUBE_SONG)
                return DirectorySanitizer.SongsFolder;


            if (resourceType == ResourceType.SONGS)
                return DirectorySanitizer.SongsFolder;

            if (resourceType == ResourceType.VIDEOS)
                return DirectorySanitizer.VideoFolder;

            if (resourceType == ResourceType.IMAGE)
                return DirectorySanitizer.VideoFolder;



            return DirectorySanitizer.OthersFolder;
        }

        public DownloadChain Downloader { set; get; }
        public WorkingSession WorkingSession =new WorkingSession();

        public WorkingSession LoadSession(bool discardExistingSessionFile = false)
        {
            
            DirectorySanitizer.Sanitize();

            var content = DirectorySanitizer.FileManager.GetContent(DirectorySanitizer.SessionFile);
            var isEmpty =
                content.TrueForAll(x =>
                {
                    var y=string.IsNullOrEmpty(x);
                    return y;
                });
            try
            {
                if (!isEmpty && !discardExistingSessionFile)
                {
                    WorkingSession =Utility. DeSerialize<WorkingSession>(DirectorySanitizer.SessionFile);
                    if (WorkingSession == null)
                    {
                        throw new Exception("Session file is corrupt");
                    }
                }
                else
                {
                    WorkingSession.DownloadList = new List<Downloads>();
                    WorkingSession.Version = 0;
                    WorkingSession.Created = DateTime.Now;
                }

                WorkingSession.LastLoaded = DateTime.Now;
                WorkingSession.SessionFileState = SessionFileState.GOOD;

                if (isEmpty || discardExistingSessionFile)
                {
                    SaveSession();
                }
            }
            catch (Exception e)
            {
                if (WorkingSession == null)
                {
                    WorkingSession=new WorkingSession()
                    {
                        SessionFileState = SessionFileState.CORRUPTED
                    };
                }

                WorkingSession.StateReason = e;
            }

            Downloader = new DownloadChain(this);
            return WorkingSession;
        }

        public bool SaveSession()
        {
            if (WorkingSession == null) throw new ArgumentNullException("workingSession");
            if (WorkingSession.SessionFileState == SessionFileState.CORRUPTED)
                return false;

            WorkingSession.Version++;
            WorkingSession.LastSaved = DateTime.Now;
            
            DirectorySanitizer.FileManager.ReplaceEveIfInExistence(DirectorySanitizer.SessionFile,"");
            Utility.Serialize(WorkingSession, DirectorySanitizer.SessionFile);
            return true;
        }
    }
}