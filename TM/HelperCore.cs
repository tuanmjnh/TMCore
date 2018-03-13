using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace TM.Helper
{
    //HttpContext
    public static class TMAppContext
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Http
        {
            get
            {
                var HttpContextAccessor = services.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor)) as Microsoft.AspNetCore.Http.IHttpContextAccessor;
                return HttpContextAccessor?.HttpContext;
            }
        }
        public static Microsoft.AspNetCore.Mvc.ActionContext Action
        {
            get
            {
                var ActionContextAccessor = services.GetService(typeof(Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor)) as Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor;
                return ActionContextAccessor?.ActionContext;
            }
        }
        public static Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingEnvironment
        {
            get
            {
                return ((Microsoft.AspNetCore.Hosting.IHostingEnvironment)Http.RequestServices.GetService(typeof(Microsoft.AspNetCore.Hosting.IHostingEnvironment)));
            }
        }
        public static string ContentRootPath
        {
            get
            {
                return HostingEnvironment.ContentRootPath;
            }
        }
        public static string WebRootPath
        {
            get
            {
                return HostingEnvironment.WebRootPath;
            }
        }
        public static string CurrentController
        {
            get
            {
                return Action.RouteData.Values["controller"].ToString();
            }
        }
        public static string CurrentAction
        {
            get
            {
                return Action.RouteData.Values["action"].ToString();
            }
        }
        //AppSettings
        public static IConfiguration configuration {
            get
            {
                var configuration = services.GetService(typeof(IConfiguration)) as IConfiguration;
                return configuration;
            }
        }
    }
    //Session Extensions
    public static class SessionExtensions
    {
        public static void Set<T>(this Microsoft.AspNetCore.Http.ISession session, string key, T value)
        {
            session.SetString(key, Newtonsoft.Json.JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this Microsoft.AspNetCore.Http.ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
    }
    //Export to AnonymousData
    public static class AnonymousData
    {
        public static System.Dynamic.ExpandoObject ToExpando(this object anonymousObject)
        {
            System.Collections.Generic.IDictionary<string, object> anonymousDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            System.Collections.Generic.IDictionary<string, object> expando = new System.Dynamic.ExpandoObject();
            foreach (var item in anonymousDictionary)
                expando.Add(item);
            return (System.Dynamic.ExpandoObject)expando;
        }
    }
    //Message
    public static class Message
    {
        public static void success(this Microsoft.AspNetCore.Mvc.Controller controller, string message)
        {
            controller.TempData["MsgSuccess"] = message;
        }
        public static void info(this Microsoft.AspNetCore.Mvc.Controller controller, string message)
        {
            controller.TempData["MsgInfo"] = message;
        }
        public static void warning(this Microsoft.AspNetCore.Mvc.Controller controller, string message)
        {
            controller.TempData["MsgWarning"] = message;
        }
        public static void danger(this Microsoft.AspNetCore.Mvc.Controller controller, string message)
        {
            controller.TempData["MsgDanger"] = message;
        }
    }
}
namespace TM.Helper.Extension
{
    //Extensions
    public static class Extensions
    {
        /// <summary>
        /// Extension method to add pagination info to Response headers
        /// </summary>
        /// <param name="response"></param>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        /// <param name="totalItems"></param>
        /// <param name="totalPages"></param>
        //public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        //{
        //    var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

        //    response.Headers.Add("Pagination",
        //       Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));
        //    // CORS
        //    response.Headers.Add("access-control-expose-headers", "Pagination");
        //}

        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            // CORS
            response.Headers.Add("access-control-expose-headers", "Application-Error");
        }
    }
}
