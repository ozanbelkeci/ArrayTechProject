using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Entities;
using static Web.ApiResult;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IWebHostEnvironment env, ICompositeViewEngine viewEngine) : base(env, viewEngine)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ControlLogin(string _username, string _userPassword)
        {
            try
            {
                var url = $"{ServiceUrl}UserService/ControlLogin";
                var paramList = new List<ServiceParameterObject>();
                paramList.Add(new ServiceParameterObject("_username", _username.ToString()));
                paramList.Add(new ServiceParameterObject("_userPassword", _userPassword.ToString()));
                var apiResult = ApiResult.SendPostRequestFromBody(url, paramList);

                if (apiResult.Status)
                {
                    var user = JsonConvert.DeserializeObject<Users>(apiResult.Message);
                    userObj = user;
                    SetCookie("FrontSideActiveUser", user.Id.ToString(), DateTime.Now.AddDays(1));
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }




        public IActionResult Logout()
        {
            RemoveCookie("FrontSideActiveUser");
            return RedirectToAction("Index", "Login");
        }
    }
}
