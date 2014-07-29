using System;

namespace muyou.Lib
{
    public class DirectorySanitizer
    {
        public AbstractDirectoryManager FileManager { set; get; }

        public AbstractDirectoryManager FolderManager { set; get; }

        protected string WorkingDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "\\" +
                                         "MyYou";

        public readonly string SongsFolder = "Songs";
        public readonly string VideoFolder = "Videos";
        public readonly string ImageFolder = "Image";
        public readonly string ZipFolder = "Zip";
        public readonly string DocsFolder = "Docs";
        public readonly string OthersFolder = "Others";

        public readonly string SessionFolder = "sessions";
        public  string TmpFolder = "tmp";

        public readonly string SessionFile = "session.ses";

        public DirectorySanitizer(AbstractDirectoryManager fileManager, AbstractDirectoryManager folderManager)
        {
            if (fileManager == null) throw new ArgumentNullException("fileManager");
            if (folderManager == null) throw new ArgumentNullException("folderManager");
            FileManager = fileManager;
            FolderManager = folderManager;


            SongsFolder = WorkingDirectory + "\\" + SongsFolder;
            VideoFolder = WorkingDirectory + "\\" + VideoFolder;
            SessionFolder = WorkingDirectory + "\\" + SessionFolder;
            TmpFolder = SessionFolder + "\\" + TmpFolder;

            ImageFolder = WorkingDirectory + "\\" + ImageFolder;
            ZipFolder = WorkingDirectory + "\\" + ZipFolder;
            DocsFolder = WorkingDirectory + "\\" + DocsFolder;
            OthersFolder = WorkingDirectory + "\\" + OthersFolder;

            SessionFile = SessionFolder + "\\" + SessionFile;
        }

        public void Sanitize()
        {
            FolderManager.CreateIfNotInExistence(WorkingDirectory);
            FolderManager.CreateIfNotInExistence(SongsFolder);
            FolderManager.CreateIfNotInExistence(VideoFolder);

            FolderManager.CreateIfNotInExistence(ImageFolder);
            FolderManager.CreateIfNotInExistence(ZipFolder);
            FolderManager.CreateIfNotInExistence(DocsFolder);

            FolderManager.CreateIfNotInExistence(OthersFolder);
          


            FolderManager.CreateIfNotInExistence(SessionFolder);
            FileManager.CreateIfNotInExistence(SessionFile);  
            
            FolderManager.CreateIfNotInExistence(TmpFolder);
        }
    }
}