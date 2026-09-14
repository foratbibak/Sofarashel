using System;
using System.Collections.Generic;
using System.Text;

namespace Sofarashel.Application.Utilities
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
