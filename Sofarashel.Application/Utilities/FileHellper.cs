using System;
using System.Collections.Generic;
using System.Text;

namespace Bibaket.Application.Utilities
{
    public class FileHellper
    {
        public static void DeletePath(string path)
        {
            if(File.Exists(path)) 
                { File.Delete(path); }
        }
    }
}
