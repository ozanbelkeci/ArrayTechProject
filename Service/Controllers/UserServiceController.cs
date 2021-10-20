using DAL.DbOperations;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service.ApiResult;

namespace Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        [HttpPost("ControlLogin")]
        public string ControlLogin([FromBody] List<ServiceParameterObject> _data)
        {

            try
            {
                var username = _data.FirstOrDefault(x => x.Key == "_username").Value;
                var password = _data.FirstOrDefault(x => x.Key == "_userPassword").Value;
                var userDB = UserDb.GetInstance();
                var result = userDB.ControlLogin(username, password);

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != null ? "Success" : "Error",
                    Message = result != null ? JsonConvert.SerializeObject(result) : "",
                    Status = result != null
                });
            }
            catch (Exception exc)
            {
                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = "Error",
                    Message = exc.Message + "," + (exc.InnerException != null ? exc.InnerException.ToString() : ""),
                    Status = false
                });
            }
        }
    }
}
