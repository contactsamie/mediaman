using System.Collections.Generic;

namespace muyou.Lib
{
    public abstract class AbstractDirectoryManager
    {
        protected abstract void DeleteResource(string resurceAddress);

        protected abstract void CreateResource(string resurceAddress, object resourceContent=null);

        public abstract bool Exists(string resurceAddress);

        protected abstract List<string> GetContentOfResource(string resurceAddress);

      
        public List<string> GetContent(string resurceAddress, bool debug = true)
        {
            return Utility.ExecuteAndHandleExceptions(() => Exists(resurceAddress) ? GetContentOfResource(resurceAddress) : new List<string>(), debug);
        }

        public void ReplaceEveIfInExistence(string resurceAddress, object resourceContent, bool debug = true)
        {
            Utility.ExecuteAndHandleExceptions(() =>
              {
                  Delete(resurceAddress);
                  CreateResource(resurceAddress, resourceContent);
                  return true;
              }, debug);
        }

        public void CreateIfNotInExistence(string resurceAddress, object resourceContent=null, bool debug = true)
        {
            Utility.ExecuteAndHandleExceptions(() =>
              {
                  if (!Exists(resurceAddress))
                  {
                      CreateResource(resurceAddress, resourceContent);
                  }
                  return true;
              }, debug);
        }

        public void Delete(string resurceAddress, bool debug = true)
        {
            Utility.ExecuteAndHandleExceptions(() =>
            {
                if (Exists(resurceAddress))
                {
                    DeleteResource(resurceAddress);
                }
                return true;
            }, debug);
        }
    }
}