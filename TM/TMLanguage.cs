using System;
namespace TM.Helper
{
    public static class Language
    {
        private static string AppLanguage = "AppLanguage";
        public static string CurrentLang { get; set; }
        private static string Section { get; set; }
        public const string DefaultLang = "vi-vn";
        private const string ext = ".json";
        private const string GlobalSection = "Global";
        private static dynamic LangList { get; set; }
        public static IServiceProvider ServiceProvider;
        //Newtonsoft.Json.Linq.JObject
        //public Language(string lang = "vi-vn.json")
        //{

        //}
        public static Newtonsoft.Json.Linq.JObject LoadJson(string lang)
        {
            //var o1 = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(@"c:\videogames.json"));

            // read JSON directly from a file
            using (var file = System.IO.File.OpenText(lang))//"~\\cms\\Language\vi-vn.json"
            using (var reader = new Newtonsoft.Json.JsonTextReader(file))
            {
                var o2 = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.Linq.JToken.ReadFrom(reader);
                return o2;
            }
        }
        public static dynamic LoadJsonToList(string lang)
        {
            var file = System.IO.File.ReadAllText(lang);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(file);
        }
        public static string ReadLanguageFile(string lang)
        {
            return System.IO.File.ReadAllText(lang);
        }
        public static string Set(string lang = null)
        {
            if (lang != null)
            {
                Language.CurrentLang = lang;
                Language.LangList = Newtonsoft.Json.JsonConvert.DeserializeObject(ReadLanguageFile(Common.Directories.languageDir + lang + ext));
            }
            else
            {
                Language.CurrentLang = Language.DefaultLang;
                Language.LangList = Newtonsoft.Json.JsonConvert.DeserializeObject(ReadLanguageFile(Common.Directories.languageDir + Language.CurrentLang + ext));
            }
            return Language.CurrentLang;
        }
        public static dynamic Get()
        {
            if (Language.LangList == null) Set();
            return Language.LangList;
        }
        public static dynamic Get(string Section)
        {
            return Get()[Section];
        }
        public static dynamic Global
        {
            get
            {
                return Get(GlobalSection);
            }
        }
        public static string Globals(string GlobalSection)
        {
            return (string)Global[GlobalSection];
        }
        public static dynamic Current
        {
            get
            {
                return Get(TMAppContext.Action.RouteData.Values["controller"].ToString());
            }
        }
        public static string Currents(string CurrentSection)
        {
            return (string)Current[CurrentSection];
        }
        public static dynamic GetCurrent(this Microsoft.AspNetCore.Mvc.Controller controller)
        {
            return Get(controller.ControllerContext.RouteData.Values["controller"].ToString());
        }
        public static dynamic GetCurrent(string controller)
        {
            return Get(controller);
        }
        //public static string SetLanguage(string lang)
        //{
        //    var options = new Microsoft.AspNetCore.Http.CookieOptions();
        //    options.Expires = DateTime.Now.AddDays(365);
        //    AppHttpContext.Current.Response.Cookies.Append(AppLanguage, ReadLanguageFile(Common.Directories.languageDir + lang + ext), options);
        //    Language.lang = lang;
        //    Language.language = Newtonsoft.Json.JsonConvert.DeserializeObject(AppHttpContext.Current.Request.Cookies[AppLanguage]);
        //    return Language.lang;
        //}
        //public static dynamic GetLanguage(string lang = null)
        //{
        //    var rs = AppHttpContext.Current.Request.Cookies[AppLanguage];
        //    if (string.IsNullOrEmpty(rs))
        //    {
        //        if (lang == null)
        //            SetLanguage(Common.Directories.languageDir + Language.lang + ext);
        //        else
        //            SetLanguage(Common.Directories.languageDir + lang + ext);
        //    }
        //    rs = AppHttpContext.Current.Request.Cookies[AppLanguage];
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject(rs);
        //}
        //public static System.Collections.Generic.Dictionary<string, string> GetLanguageList()
        //{
        //    var language = new System.Collections.Generic.Dictionary<string, string>();
        //    foreach (var item in TM.Helper.IO.Files("Language"))
        //        language.Add(item.Name, Get(item.Name, "languageName", "global"));
        //    return language;
        //}
        //public static string Get(string lang, string key, string Section, bool RemoveAlt = true)
        //{
        //    try
        //    {
        //        var ini = new TM.Helper.INIFile("Language/" + lang);
        //        if (RemoveAlt)
        //            return ini.Read(key, Section).Replace("_", "");
        //        else
        //            return ini.Read(key, Section);
        //    }
        //    catch (Exception) { return null; }
        //}
        //public static string Get(string key, string Section, bool RemoveAlt = true)
        //{
        //    return Get(lang, key, Section, RemoveAlt);
        //}
        //public static string Get(string lang, string key, string Section)
        //{
        //    return Get(lang, key, Section, false);
        //}
        //public static string Get(string key, string Section)
        //{
        //    return Get(lang, key, Section);
        //}
        //public static string Get(string key, bool RemoveAlt = true)
        //{
        //    return Get(lang, key, Section, RemoveAlt);
        //}
        //public static string Get(string key)
        //{
        //    return Get(lang, key, Section, false);
        //}
    }
}
