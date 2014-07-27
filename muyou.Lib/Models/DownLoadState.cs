namespace muyou.Lib
{
    public enum DownLoadState
    {
        UNKNOWN=0,
        NOT_YET_DOWNLOADED,
        UNABLE_TO_DOWNLOAD,
        IN_PROGRESS,
        DOWNLOADED,
        VERIFIED_DOWNLOADED,
        DOWNLOD_NOT_FOUND_AFTER_DOWNLOAD,
        CORRUPTED_DOWNLOAD
    }
}