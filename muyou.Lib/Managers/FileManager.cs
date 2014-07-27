using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace muyou.Lib
{
    public class FileManager : AbstractDirectoryManager
    {
        protected override void DeleteResource(string resurceAddress)
        {
            if (System.IO.File.Exists(resurceAddress))
            System.IO.File.Delete(resurceAddress);
         
            
        }

        protected override void CreateResource(string resurceAddress, object resourceContent=null)
        {
            System.IO.File.Create(resurceAddress).Close();
            if (resourceContent != null) System.IO.File.WriteAllText(resurceAddress, resourceContent.ToString());


 //           using (var fs= new FileStream(resurceAddress,FileMode.Create,FileAccess.ReadWrite))
 //{ 
                
 //  fs.Close();
 //}

 //           using (StreamWriter writer = new StreamWriter(resurceAddress))
 //           {
 //               writer.Write(resurceAddress,resourceContent??"");
 //               writer.Close();
 //           }
        }

        public override bool Exists(string resurceAddress)
        {
            return System.IO.File.Exists(resurceAddress);

           
        }

        protected override System.Collections.Generic.List<string> GetContentOfResource(string resurceAddress)
        {
            return System.IO.File.ReadAllLines(resurceAddress).ToList();
            //using (var fs = new FileStream(resurceAddress, FileMode.Create, FileAccess.ReadWrite))
            //{
            //    fs.Close();
            //}
            //var lines = new List<string>();
            //using (StreamReader reader = new StreamReader(resurceAddress))
            //{
            //   lines= reader.ReadToEnd().Split('\n').ToList();
            //    reader.Close();
            //}
            //return lines;
        }
    }
}