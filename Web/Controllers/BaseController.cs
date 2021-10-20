using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web.Entities;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected ICompositeViewEngine viewEngine;

        protected IWebHostEnvironment _env;

        public BaseController(IWebHostEnvironment env, ICompositeViewEngine viewEngine)
        {
            this.viewEngine = viewEngine;
            this._env = env;
        }

        public string ServiceUrl = "https://localhost:44312/";

        public Users userObj = null;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var cookieUserId = "";
                var cookie = "";

                cookie = GetCookie("FrontSideActiveUser");

               
                if (!filterContext.HttpContext.Request.Query.ContainsKey("AutoMailReport"))
                {
                  

                    bool isAjaxCall = filterContext.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";

                    if (isAjaxCall == false)
                    {

                        if (cookie != null)
                        {
                            
                        }
                        else
                        {
                            var controllerName = filterContext.Controller.ToString().Replace("Web.Controllers.", "").Replace("Controller", "");
                            if (controllerName != "Login")
                            {
                                filterContext.Result =
                             new RedirectToRouteResult(
                                 new RouteValueDictionary{{ "controller", "Login" }, { "action", "Index" }
                             });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public string GetCookie(string key)
        {

            var val = Request.Cookies[key];
            if (val == null)
            {
                return null;
            }
           

            var plainTextData = Decrypt(val);

            return plainTextData;
        }

        public void SetCookie(string key, string value, DateTime dt)
        {
            try
            {

                var encryptVal = Encrypt(value);

                CookieOptions option = new CookieOptions();
                option.Expires = dt;

                Response.Cookies.Append(key, encryptVal, option);




            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public void RemoveCookie(string key)
        {
            Response.Cookies.Delete(key);
        }

        private const string mysecurityKey = "exampleProject";

        public static string Encrypt(string TextToEncrypt)
        {
            byte[] MyEncryptedArray = UTF8Encoding.UTF8
               .GetBytes(TextToEncrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new
               MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash
               (UTF8Encoding.UTF8.GetBytes(mysecurityKey));

            MyMD5CryptoService.Clear();

            var MyTripleDESCryptoService = new
               TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService
               .CreateEncryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyEncryptedArray, 0,
               MyEncryptedArray.Length);

            MyTripleDESCryptoService.Clear();

            return Convert.ToBase64String(MyresultArray, 0,
               MyresultArray.Length);
        }



        public static string Decrypt(string TextToDecrypt)
        {
            byte[] MyDecryptArray = Convert.FromBase64String
               (TextToDecrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new
               MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash
               (UTF8Encoding.UTF8.GetBytes(mysecurityKey));

            MyMD5CryptoService.Clear();

            var MyTripleDESCryptoService = new
               TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService
               .CreateDecryptor();

            byte[] MyresultArray = MyCrytpoTransform
               .TransformFinalBlock(MyDecryptArray, 0,
               MyDecryptArray.Length);

            MyTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(MyresultArray);
        }

    }
}
