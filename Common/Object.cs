namespace TM.Common
{
    public class Extensions
    {
        public static readonly string[] Images = new[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp" };
        public static readonly string[] Excel = new[] { ".xls", ".xlsx", ".csv" };
    }
    public class Directories
    {
        //Root
        public const string RootDir = "wwwroot\\";
        //Uploads
        public const string Uploads = "Uploads\\";
        public const string data = Uploads + "Data\\";
        public const string HDData = data + "HDData\\";
        public const string images = Uploads + "Images\\";
        public const string imagesProduct = images + "Product\\";
        public const string imagesCustomer = images + "Customer\\";
        public const string document = data + "Document\\";
        public const string orther = Uploads + "Orther\\";
        public const string ccbs = data + "ccbs\\";
        public const string DBBak = Uploads + "DBBak\\";
        //Language Directories
        public const string languageDir = RootDir + "Language\\";
    }
    public class FileManager
    {
        public const string directory = "Directory";
        public const string file = "File";
    }
    public class Success
    {
        public const string Get = "Global.msgGetSucsess";
        public const string Create = "Global.msgCreateSucsess";
        public const string Update = "Global.msgUpdateSucsess";
        public const string Delete = "Global.msgDeleteSucsess";
        public const string Recover = "Global.msgRecoverSucsess";
    }
    public class Error
    {
        public const string DB = "Global.msgDBError";
        public const string Model = "Global.msgModelError";
        public const string IDExist = "Global.msgIDExistError";
        public const string IDNull = "Global.msgIDNullError";
        public const string Null = "Global.msgNullError";
        public const string Create = "Global.msgCreateError";
        public const string Update = "Global.msgUpdateError";
        public const string Delete = "Global.msgDeleteError";
        public const string Recover = "Global.msgRecoverError";
        public const string Exist = "Global.msgErrorExist";
    }
    public class Permission
    {
        public const string Select = "GET";
        public const string Create = "POST";
        public const string Edit = "PUT";
        public const string Delete = "DELETE";
    }
}