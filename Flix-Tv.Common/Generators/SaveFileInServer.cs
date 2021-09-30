using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flix_Tv.Common.Generators
{
   public class SaveFileInServer
    {
        public async Task<string> SaveFile(string path,string fileName,IFormFile file)
        {
            string pathName = Path.Combine(Directory.GetCurrentDirectory(), path,fileName);
            using (var stream = new FileStream(pathName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return pathName;
        }
    }
}
