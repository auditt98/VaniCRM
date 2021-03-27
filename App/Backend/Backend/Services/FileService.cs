using Backend.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Backend.Services
{
    public class FileService
    {
        FileRepository _fileRepository = new FileRepository();
        public (ByteArrayContent file, string mimeType, string fileName) Download(int id)
        {
            var dbFile = _fileRepository.GetOne(id);
            if(dbFile != null)
            {
                string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
                byte[] content = null;
                using (FileStream fs = File.Open(Path.Combine(targetFolder, dbFile.FileName), FileMode.Open))
                {
                    content = new byte[fs.Length];
                    fs.Read(content, 0, (int)fs.Length);
                }
                var mimeType = MimeMapping.MimeUtility.GetMimeMapping(dbFile.FileName);
                return (new ByteArrayContent(content), mimeType, dbFile.FileName);
            }
            else
            {
                return (null, null, null);
            }
        }

    }
}