using System;
//using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TM.Helper
{
    public class IO
    {
        public static string MapPath(string path)
        {
            return $@"{TM.Helper.TMAppContext.WebRootPath}\{path}";
        }
        public static bool SetAccessRule(string directory, bool IsMapPath = true)
        {
            var Rights = (System.Security.AccessControl.FileSystemRights)0;
            Rights = System.Security.AccessControl.FileSystemRights.FullControl;
            // *** Add Access Rule to the actual directory itself
            var AccessRule = new System.Security.AccessControl.FileSystemAccessRule("Users", Rights,
                                        System.Security.AccessControl.InheritanceFlags.None,
                                        System.Security.AccessControl.PropagationFlags.NoPropagateInherit,
                                        System.Security.AccessControl.AccessControlType.Allow);

            directory = IsMapPath ? MapPath(directory) : directory;
            DirectoryInfo Info = new DirectoryInfo(directory);
            var Security = Info.GetAccessControl(System.Security.AccessControl.AccessControlSections.Access);
            bool Result = false;
            Security.ModifyAccessRule(System.Security.AccessControl.AccessControlModification.Set, AccessRule, out Result);

            if (!Result)
                return false;
            // *** Always allow objects to inherit on a directory
            var iFlags = System.Security.AccessControl.InheritanceFlags.ObjectInherit;
            iFlags = System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit;
            // *** Add Access rule for the inheritance
            AccessRule = new System.Security.AccessControl.FileSystemAccessRule("Users", Rights,
                                        iFlags,
                                        System.Security.AccessControl.PropagationFlags.InheritOnly,
                                        System.Security.AccessControl.AccessControlType.Allow);
            Result = false;
            Security.ModifyAccessRule(System.Security.AccessControl.AccessControlModification.Add, AccessRule, out Result);
            if (!Result)
                return false;
            Info.SetAccessControl(Security);
            return true;
        }
        public static bool Rename(string sourceFile, string DestFile, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                DestFile = IsMapPath ? MapPath(DestFile) : DestFile;
                File.Move(sourceFile, DestFile);
                return true;
            }
            catch (Exception) { return false; }
        }
        public static FileInfo ReExtension(string sourceFile, string extension, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                var file = new FileInfo(sourceFile);
                var DestFile = sourceFile.Replace(file.Extension, extension);
                File.Move(sourceFile, DestFile);
                return new FileInfo(DestFile);
            }
            catch (Exception) { return null; }
        }
        public static FileInfo ReExtensionToLower(string sourceFile, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                var file = new FileInfo(sourceFile);
                var DestFile = sourceFile.Replace(file.Extension, file.Extension.ToLower());
                File.Move(sourceFile, DestFile);
                return new FileInfo(DestFile);
            }
            catch (Exception) { return null; }
        }
        public static bool Copy(string sourceFile, string DestFile, bool IsMapPath = true)
        {
            try
            {
                sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                DestFile = IsMapPath ? MapPath(DestFile) : DestFile;
                File.Copy(sourceFile, DestFile);
                return true;
            }
            catch (Exception) { return false; }
        }
        public static bool Copy(string sourceFile)
        {
            return Copy(sourceFile, CreateFileExist(sourceFile));
        }
        public static bool Delete(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else return true;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, string[] files, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(path + item))
                        File.Delete(path + item);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, FileInfo[] files, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(path + item.Name))
                        File.Delete(path + item.Name);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool DeleteDirectory(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                if (Directory.Exists(path))
                {
                    foreach (var item in Files(path, false))
                        File.Delete(item.FullName);
                    Directory.Delete(path);
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool CreateDirectory(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                path = path.Trim('/', '\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    var directory = new DirectoryInfo(path);
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }
        public static string CreateFileExist(string file, bool IsMapPath = true)
        {
            try
            {
                int countFile = 0;
                file = IsMapPath ? MapPath(file) : file;
                string extension = Path.GetExtension(MapPath(file));
                while (File.Exists(file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension))
                    countFile++;
                file = file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension;
                return file;
            }
            catch (Exception) { throw; }
        }
        public static byte[] ReturnByteFile(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                byte[] fileBytes = File.ReadAllBytes(path);
                return fileBytes;
            }
            catch (Exception) { throw; }
        }
        public static DirectoryInfo[] Directories(string path, bool IsMapPath = true)
        {
            try
            {
                path = IsMapPath ? MapPath(path) : path;
                var Dir = new DirectoryInfo(path);
                return Dir.GetDirectories();
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.Generic.List<string> DirectoriesToList(string path, bool IsMapPath = true)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                var subDir = Directories(path, IsMapPath);
                foreach (var item in subDir)
                    list.Add(item.Name);
                return list;
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, string[] extension = null, bool IsMapPath = true)
        {
            try
            {
                //var files = System.IO.Directory.GetDirectories(path);
                //string[] ext = new[] { ".dbf" };
                path = IsMapPath ? MapPath(path) : path;
                var Dir = new DirectoryInfo(path);
                if (extension != null)
                    return Dir.GetFiles().Where(f => extension.Contains(f.Extension.ToLower())).ToArray();
                else
                    return Dir.GetFiles();
                //var subFiles = di.GetFiles("*.dbf").Concat(di.GetFiles("*.dbf2"));
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, bool IsMapPath = true)
        {
            return Files(path, null, IsMapPath);
        }
        public static System.Collections.Generic.List<string> FilesToList(string path, string[] extension, bool IsMapPath = true)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                var subFiles = Files(path, extension, IsMapPath);
                foreach (var item in subFiles)
                    list.Add(item.Name.Replace(item.Extension, item.Extension.ToLower()));
                return list;
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.Generic.List<string> FilesToList(string path, bool IsMapPath = true)
        {
            return FilesToList(path, null, IsMapPath);
        }
        public static string[] ReadFile(string filename)
        {

            var file = MapPath(filename);
            var list = System.IO.File.ReadAllLines(file);
            return list;
        }
        public static System.Collections.Generic.List<string[]> ReadFile(string filename, char split)
        {
            var rs = new System.Collections.Generic.List<string[]>();
            foreach (var item in ReadFile(filename))
            {
                var tmp = item.Split(split);
                rs.Add(tmp);
            }
            return rs;
        }
        //public static System.Collections.Generic.List<string> ImageCodecs()
        //{
        //    //var rs = new System.Collections.Generic.List<string>();
        //    //foreach (var item in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().Select(codec => codec.FilenameExtension).ToList())
        //    //    rs.Add(item.TrimStart('*'));
        //    //return rs;
        //    return System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().Select(codec => codec.FilenameExtension).ToList();
        //}

    }
    
    //public class Zip
    //{
    //    public static void CompressFolder(string path, ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream, int folderOffset)
    //    {

    //        string[] files = Directory.GetFiles(path);

    //        foreach (string filename in files)
    //        {

    //            FileInfo fi = new FileInfo(filename);

    //            string entryName = filename.Substring(folderOffset); // Makes the name in zip based on the folder
    //            entryName = ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(entryName); // Removes drive from name and fixes slash direction
    //            var newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(entryName);
    //            newEntry.DateTime = fi.LastWriteTime; // Note the zip format stores 2 second granularity

    //            // Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
    //            // A password on the ZipOutputStream is required if using AES.
    //            //   newEntry.AESKeySize = 256;

    //            // To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
    //            // you need to do one of the following: Specify UseZip64.Off, or set the Size.
    //            // If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
    //            // but the zip will be in Zip64 format which not all utilities can understand.
    //            //   zipStream.UseZip64 = UseZip64.Off;
    //            newEntry.Size = fi.Length;

    //            zipStream.PutNextEntry(newEntry);

    //            // Zip the file in buffered chunks
    //            // the "using" will close the stream even if an exception occurs
    //            byte[] buffer = new byte[4096];
    //            using (FileStream streamReader = File.OpenRead(filename))
    //            {
    //                ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(streamReader, zipStream, buffer);
    //            }
    //            zipStream.CloseEntry();
    //        }
    //        string[] folders = Directory.GetDirectories(path);
    //        foreach (string folder in folders)
    //        {
    //            CompressFolder(folder, zipStream, folderOffset);
    //        }
    //    }
    //    public static void CreateSample(string outPathname, string password, string folderName)
    //    {

    //        FileStream fsOut = File.Create(outPathname);
    //        var zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(fsOut);

    //        zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

    //        zipStream.Password = password;  // optional. Null is the same as not setting. Required if using AES.

    //        // This setting will strip the leading part of the folder path in the entries, to
    //        // make the entries relative to the starting folder.
    //        // To include the full path for each entry up to the drive root, assign folderOffset = 0.
    //        int folderOffset = folderName.Length + (folderName.EndsWith("\\") ? 0 : 1);

    //        CompressFolder(folderName, zipStream, folderOffset);

    //        zipStream.IsStreamOwner = true; // Makes the Close also Close the underlying stream
    //        zipStream.Close();
    //    }
    //    public static MemoryStream CreateToMemoryStream(MemoryStream memStreamIn, string zipEntryName)
    //    {

    //        MemoryStream outputMemStream = new MemoryStream();
    //        var zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outputMemStream);

    //        zipStream.SetLevel(3); //0-9, 9 being the highest level of compression

    //        var newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(zipEntryName);
    //        newEntry.DateTime = DateTime.Now;

    //        zipStream.PutNextEntry(newEntry);

    //        ICSharpCode.SharpZipLib.Core.StreamUtils.Copy(memStreamIn, zipStream, new byte[4096]);
    //        zipStream.CloseEntry();

    //        zipStream.IsStreamOwner = false;    // False stops the Close also Closing the underlying stream.
    //        zipStream.Close();          // Must finish the ZipOutputStream before using outputMemStream.

    //        outputMemStream.Position = 0;
    //        return outputMemStream;

    //        // Alternative outputs:
    //        // ToArray is the cleaner and easiest to use correctly with the penalty of duplicating allocated memory.
    //        byte[] byteArrayOut = outputMemStream.ToArray();

    //        // GetBuffer returns a raw buffer raw and so you need to account for the true length yourself.
    //        //byte[] byteArrayOut = outputMemStream.GetBuffer();
    //        long len = outputMemStream.Length;
    //    }
    //    public static void DownloadZipToBrowser(System.Collections.Generic.List<string> zipFileList)
    //    {

    //        System.Web.HttpContext.Current.Response.ContentType = "application/zip";
    //        // If the browser is receiving a mangled zipfile, IIS Compression may cause this problem. Some members have found that
    //        //Response.ContentType = "application/octet-stream" has solved this. May be specific to Internet Explorer.

    //        System.Web.HttpContext.Current.Response.AppendHeader("content-disposition", "attachment; filename=\"Download.zip\"");
    //        System.Web.HttpContext.Current.Response.CacheControl = "Private";
    //        System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(5)); // or put a timestamp in the filename in the content-disposition

    //        byte[] buffer = new byte[4096];

    //        var zipOutputStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(System.Web.HttpContext.Current.Response.OutputStream);
    //        zipOutputStream.SetLevel(3); //0-9, 9 being the highest level of compression

    //        foreach (string fileName in zipFileList)
    //        {

    //            Stream fs = File.OpenRead(TM.IO.FileDirectory.MapPath(fileName));    // or any suitable inputstream
    //            var entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(ICSharpCode.SharpZipLib.Zip.ZipEntry.CleanName(fileName));
    //            entry.Size = fs.Length;
    //            // Setting the Size provides WinXP built-in extractor compatibility,
    //            //  but if not available, you can set zipOutputStream.UseZip64 = UseZip64.Off instead.

    //            zipOutputStream.PutNextEntry(entry);

    //            int count = fs.Read(buffer, 0, buffer.Length);
    //            while (count > 0)
    //            {
    //                zipOutputStream.Write(buffer, 0, count);
    //                count = fs.Read(buffer, 0, buffer.Length);
    //                if (!System.Web.HttpContext.Current.Response.IsClientConnected)
    //                {
    //                    break;
    //                }
    //                System.Web.HttpContext.Current.Response.Flush();
    //            }
    //            fs.Close();
    //        }
    //        zipOutputStream.Close();

    //        System.Web.HttpContext.Current.Response.Flush();
    //        System.Web.HttpContext.Current.Response.End();
    //    }
    //    public static MemoryStream CopyStream(Stream input)
    //    {
    //        var output = new MemoryStream();
    //        byte[] buffer = new byte[16 * 1024];
    //        int read;
    //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
    //        {
    //            output.Write(buffer, 0, read);
    //        }
    //        return output;
    //    }
    //    //
    //    public static byte[] Compress(byte[] data, string fileName)
    //    {
    //        // Compress
    //        using (MemoryStream fsOut = new MemoryStream())
    //        {
    //            using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(fsOut))
    //            {
    //                zipStream.SetLevel(3);
    //                ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);
    //                newEntry.DateTime = DateTime.UtcNow;
    //                newEntry.Size = data.Length;
    //                zipStream.PutNextEntry(newEntry);
    //                zipStream.Write(data, 0, data.Length);
    //                zipStream.Finish();
    //                zipStream.Close();
    //            }
    //            return fsOut.ToArray();
    //        }
    //    }
    //    public static void Compress(Stream data, Stream outData, string fileName)
    //    {
    //        string str = "";
    //        try
    //        {
    //            using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outData))
    //            {
    //                zipStream.SetLevel(3);
    //                ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);
    //                newEntry.DateTime = DateTime.UtcNow;
    //                zipStream.PutNextEntry(newEntry);
    //                data.Position = 0;
    //                int size = (data.CanSeek) ? Math.Min((int)(data.Length - data.Position), 0x2000) : 0x2000;
    //                byte[] buffer = new byte[size];
    //                int n;
    //                do
    //                {
    //                    n = data.Read(buffer, 0, buffer.Length);
    //                    zipStream.Write(buffer, 0, n);
    //                } while (n != 0);
    //                zipStream.CloseEntry();
    //                zipStream.Flush();
    //                zipStream.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            str = ex.Message;
    //        }

    //    }
    //    public static void Compress2(Stream data, Stream outData, string fileName)
    //    {
    //        string str = "";
    //        try
    //        {
    //            using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(outData))
    //            {
    //                zipStream.SetLevel(3);
    //                ICSharpCode.SharpZipLib.Zip.ZipEntry newEntry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName);
    //                newEntry.DateTime = DateTime.UtcNow;
    //                zipStream.PutNextEntry(newEntry);
    //                data.CopyTo(zipStream);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            str = ex.Message;
    //        }

    //    }
    //}
}
public static class IOS
{
    public static string ToExtension(this string file)
    {
        try
        {
            return Path.GetExtension(file);
        }
        catch (Exception) { throw; }
    }
    public static string ToExtensionNone(this string file)
    {
        return ToExtension(file).Trim('.');
    }
    public static bool IsExtension(this string file, string Extension)
    {
        try
        {
            if (file.ToExtension().ToLower() == (Extension[0].ToString() == "." ? Extension.ToLower() : "." + Extension.ToLower()))
                return true;
            else return false;
        }
        catch (Exception) { throw; }
    }
    public static bool IsExtension(this string file, string[] Extension)
    {
        if (Extension.Length > 0)
            foreach (var item in Extension)
                if (file.IsExtension(item)) return true;
        return false;
    }
    public static System.Collections.Generic.List<string> UploadFileSource(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadFileSource"];
        }
        catch (Exception)
        {
            return null;
        }

    }
    public static string UploadFileSourceString(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (string)Upload["UploadFileSourceString"];
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static System.Collections.Generic.List<string> UploadFile(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadFile"];
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static string UploadFileString(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (string)Upload["UploadFileString"];
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static System.Collections.Generic.List<string> UploadError(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadError"];
        }
        catch (Exception)
        {
            return null;
        }
    }
}
