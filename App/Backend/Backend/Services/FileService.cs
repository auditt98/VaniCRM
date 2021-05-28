using Backend.Extensions;
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

        public (ByteArrayContent file, string mimeType, string fileName) GetAvatar(string avatarFileName)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            var f = new FileManager.File();
            f.FileName = Path.GetFileNameWithoutExtension(avatarFileName);
            f.Extension = Path.GetExtension(avatarFileName);
            var isImage = f.FilterExtension(new List<string>() { ".jpeg", ".jpg", ".png", ".tif", ".tiff" });
            if (isImage)
            {
                var isExist = File.Exists(Path.Combine(targetFolder, avatarFileName));
                byte[] content = null;
                if (isExist)
                {
                    using (FileStream fs = File.Open(Path.Combine(targetFolder, avatarFileName), FileMode.Open))
                    {
                        content = new byte[fs.Length];
                        fs.Read(content, 0, (int)fs.Length);
                    }
                    var mimeType = MimeMapping.MimeUtility.GetMimeMapping(avatarFileName);
                    return (new ByteArrayContent(content), mimeType, avatarFileName);
                }
                else
                {
                    return (null, null, null);
                }
            }
            else
            {
                return (null, null, null);
            }
        }

        public (ByteArrayContent file, string mimeType, string fileName) GetFile(string fileName)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            var f = new FileManager.File();
            f.FileName = Path.GetFileNameWithoutExtension(fileName);
            f.Extension = Path.GetExtension(fileName);

            var isExist = File.Exists(Path.Combine(targetFolder, fileName));
            byte[] content = null;
            if (isExist)
            {
                using (FileStream fs = File.Open(Path.Combine(targetFolder, fileName), FileMode.Open))
                {
                    content = new byte[fs.Length];
                    fs.Read(content, 0, (int)fs.Length);
                }
                var mimeType = MimeMapping.MimeUtility.GetMimeMapping(fileName);
                return (new ByteArrayContent(content), mimeType, fileName);
            }
            else
            {
                return (null, null, null);
            }

        }
    }
}