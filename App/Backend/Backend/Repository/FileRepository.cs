using Backend.Domain;
using Backend.Extensions;
using ByteSizeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static Backend.Extensions.FileManager;

namespace Backend.Repository
{
    public class FileRepository
    {
        DatabaseContext db = new DatabaseContext();
        public FILE GetOne(int id)
        {
            return db.FILEs.Find(id);
        }

        public FILE Create(FileManager.File file)
        {
            var newFile = new FILE();
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            file.Rename();
            file.Save(targetFolder);
            newFile.FileName = file.FullName;
            var size = ByteSize.FromBytes(file.ContentLengthBytes);
            newFile.FileSize = Convert.ToInt32(size.KiloBytes);
            newFile.FileStorePath = Path.Combine(targetFolder, file.FullName);
            db.FILEs.Add(newFile);
            db.SaveChanges();
            return newFile;
        }
    }
}