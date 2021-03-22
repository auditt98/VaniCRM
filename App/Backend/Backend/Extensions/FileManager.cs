using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    public class FileManager
    {
        public List<File> Files { get; set; }
        public class File
        {
            public string FileName { get; set; }
            public int ContentLengthBytes { get; set; }
            public string SavedPath { get; set; }
            public string Content { get; set; }
            public string Extension { get; set; }
            public string MimeType { get; set; }
            public string FullName { get; set; }
            public HttpPostedFile file { get; set; }

            public File() { }

            public File(HttpPostedFile f)
            {
                this.FileName = Path.GetFileNameWithoutExtension(f.FileName);
                this.MimeType = f.ContentType;
                this.ContentLengthBytes = f.ContentLength;
                this.Extension = Path.GetExtension(f.FileName);
                this.FullName = FileName + Extension;
                this.file = f;
            }

            public File Save(string saveTo)
            {
                var combinedPath = Path.Combine(saveTo, FullName);
                file.SaveAs(combinedPath);
                this.SavedPath = Path.Combine(combinedPath);
                return this;
            }

            public File ReadPosted()
            {
                Stream fs = file.InputStream;
                BinaryReader br = new BinaryReader(fs);
                Byte[] bytes = br.ReadBytes((int)fs.Length);
                Content = Convert.ToBase64String(bytes, 0, bytes.Length);
                return this;
            }

            public bool FilterExtension(List<string> extension)
            {
                if (extension.Contains(this.Extension))
                {
                    return true;
                }
                return false;
            }

            public File Rename()
            {
                this.FileName = string.Format("{0}-{1}", FileName, Guid.NewGuid().ToString("N"));
                this.FullName = string.Format("{0}{1}", FileName, Extension);
                return this;

            }

        }

        

    }
}