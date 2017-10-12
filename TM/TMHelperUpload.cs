using System;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace TM.Helper
{
    public class Upload
    {
        //Declares
        Microsoft.AspNetCore.Http.IFormFileCollection files;
        string uploadDir;
        string[] Extension;
        bool Rename = true;
        int MaxFileCount = 5;

        public Upload(Microsoft.AspNetCore.Http.IFormFileCollection files, string uploadDir, string[] Extension, bool Rename = true, int MaxFileCount = 5)
        {
            this.files = files;
            this.uploadDir = uploadDir.Trim('\\');
            this.Extension = Extension;
            this.Rename = Rename;
            this.MaxFileCount = MaxFileCount;
        }
        public Upload(Microsoft.AspNetCore.Http.IFormFileCollection files, string uploadDir, bool Rename, int MaxFileCount = 5)
        {
            this.files = files;
            this.uploadDir = uploadDir.Trim('\\');
            this.Extension = null;
            this.Rename = Rename;
            this.MaxFileCount = MaxFileCount;
        }
        public Upload(Microsoft.AspNetCore.Http.IFormFileCollection files, string uploadDir, bool Rename)
        {
            this.files = files;
            this.uploadDir = uploadDir.Trim('\\');
            this.Extension = null;
            this.Rename = Rename;
        }
        public Upload(Microsoft.AspNetCore.Http.IFormFileCollection files, string uploadDir, string[] Extension)
        {
            this.files = files;
            this.uploadDir = uploadDir.Trim('\\');
            this.Extension = Extension;
        }
        public Upload(Microsoft.AspNetCore.Http.IFormFileCollection files, string uploadDir)
        {
            this.files = files;
            this.uploadDir = uploadDir.Trim('\\');
        }

        //File Size Name
        public System.Collections.Generic.Dictionary<long, string> FileSizeName()
        {
            var rs = new System.Collections.Generic.Dictionary<long, string>();
            if (files.Count > 0)
            {
                //Create Directory Upload
                var UploadsDir = TM.Helper.IO.MapPath(uploadDir);
                TM.Helper.IO.CreateDirectory(UploadsDir, false);
                //Upload File
                long size = 0;
                for (int i = 0; i < files.Count; i++)
                {
                    if (MaxFileCount > 0)
                        if (i >= MaxFileCount)
                            break;

                    size = files[i].Length;
                    if (size < 1) continue;
                    var filename = ContentDispositionHeaderValue.Parse(files[i].ContentDisposition).FileName.ToString().Trim('"');
                    if (Rename) filename = (Guid.NewGuid().ToString("N") + filename.ToExtension()).ToLower();
                    rs.Add(size, filename);

                    if (Extension != null)
                        if (!filename.IsExtension(Extension))
                        {
                            //UploadError.Add("Extension:" + file.FileName);
                            continue;
                        }

                    filename = $@"{UploadsDir}\{filename}";

                    using (FileStream fs = File.Create(filename))
                    {
                        files[i].CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
            return rs;
        }
        //Images Size Name
        public System.Collections.Generic.Dictionary<long, string> ImagesSizeName()
        {
            this.Extension = Common.Extensions.Images;
            return FileSizeName();
        }
        //File Size FullName
        public System.Collections.Generic.Dictionary<long, string> FileSizeFullName()
        {
            var rs = new System.Collections.Generic.Dictionary<long, string>();
            if (files.Count > 0)
            {
                //Create Directory Upload
                var UploadsDir = TM.Helper.IO.MapPath(uploadDir);
                TM.Helper.IO.CreateDirectory(UploadsDir, false);
                //Upload File
                long size = 0;
                for (int i = 0; i < files.Count; i++)
                {
                    if (MaxFileCount > 0)
                        if (i >= MaxFileCount)
                            break;

                    size = files[i].Length;
                    if (size < 1) continue;
                    var filename = ContentDispositionHeaderValue.Parse(files[i].ContentDisposition).FileName.ToString().Trim('"');
                    if (Rename) filename = (Guid.NewGuid().ToString("N") + filename.ToExtension()).ToLower();


                    if (Extension != null)
                        if (!filename.IsExtension(Extension))
                        {
                            //UploadError.Add("Extension:" + file.FileName);
                            continue;
                        }

                    filename = $@"{UploadsDir}\{filename}";
                    rs.Add(size, filename);

                    using (FileStream fs = File.Create(filename))
                    {
                        files[i].CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
            return rs;
        }
        //Images Size FullName
        public System.Collections.Generic.Dictionary<long, string> ImagesSizeFullName()
        {
            this.Extension = Common.Extensions.Images;
            return FileSizeFullName();
        }
        //File Name
        public System.Collections.Generic.List<string> FileName()
        {
            var rs = new System.Collections.Generic.List<string>();
            foreach (var item in FileSizeName())
                rs.Add(item.Value);
            return rs;
        }
        //Images Name
        public System.Collections.Generic.List<string> ImagesName()
        {
            this.Extension = Common.Extensions.Images;
            return FileName();
        }
        //File FullName
        public System.Collections.Generic.List<string> FileFullName()
        {
            var rs = new System.Collections.Generic.List<string>();
            foreach (var item in FileSizeFullName())
                rs.Add(item.Value);
            return rs;
        }
        //Images FullName
        public System.Collections.Generic.List<string> ImagesFullName()
        {
            this.Extension = Common.Extensions.Images;
            return FileFullName();
        }
    }
}
