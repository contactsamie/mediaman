using System.Linq;

namespace muyou.Lib
{
    public class FolderManager : AbstractDirectoryManager
    {
        protected override void DeleteResource(string resurceAddress)
        {
            System.IO.Directory.Delete(resurceAddress);
        }

        protected override void CreateResource(string resurceAddress, object resourceContent=null)
        {
            System.IO.Directory.CreateDirectory(resurceAddress);
        }

        public override bool Exists(string resurceAddress)
        {
            return System.IO.Directory.Exists(resurceAddress);
        }

        protected override System.Collections.Generic.List<string> GetContentOfResource(string resurceAddress)
        {
            var folders = System.IO.Directory.GetDirectories(resurceAddress).ToList();
            var files = System.IO.Directory.GetFiles(resurceAddress).ToList();
            folders.AddRange(files);

            return folders;
        }
    }
}