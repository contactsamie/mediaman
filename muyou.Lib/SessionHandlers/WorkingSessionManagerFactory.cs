namespace muyou.Lib
{
    public class WorkingSessionManagerFactory
    {
        public WorkingSessionManager CreateWorkingSessionManager(AbstractDirectoryManager fileManager,
            AbstractDirectoryManager folderManager)
        {
            return new WorkingSessionManager(new DirectorySanitizer(fileManager, folderManager));
        }

        public WorkingSessionManager CreateWorkingSessionManager()
        {
            return CreateWorkingSessionManager(new FileManager(), new FolderManager());
        }
    }
}