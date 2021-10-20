using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Web.Entities;
using Web.Models;
using static Web.ApiResult;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IWebHostEnvironment env, ICompositeViewEngine viewEngine) : base(env, viewEngine)
        {
        }
        public IActionResult Index()
        {
            var url = $"{ServiceUrl}ProductsService/GetProductsList";
            var paramList = new List<ServiceParameterObject>();

            var apiResult = ApiResult.SendPostRequestFromBody(url, paramList);

            if (apiResult.Status)
            {
                var deserialiazed = JsonConvert.DeserializeObject<List<Products>>(apiResult.Message);

                return View(deserialiazed);
            }

            else
            {
                return View(new List<Products>());
            }
        }

        public JsonResult AddNewProduct(string _productName, string _dateTime, int _quantity)
        {
            var products = new Products()
            {
                ProductName = _productName,
                AddedDate = Convert.ToDateTime(_dateTime),
                Quantity = _quantity
            };

            var url = $"{ServiceUrl}ProductsService/AddNewProduct";
            var paramList = new List<ServiceParameterObject>();

            paramList.Add(new ServiceParameterObject("_product", JsonConvert.SerializeObject(products)));

            var apiResult = ApiResult.SendPostRequestFromBody(url, paramList);

            if (apiResult.Status == true)
            {
                return Json(apiResult.Status);

            }
            else
            {
                return Json(false);
            }

        }

        public JsonResult UpdateProduct(int _id,string _productName, string _dateTime, int _quantity)
        {
            var products = new Products()
            {
                Id = _id,
                ProductName = _productName,
                AddedDate = Convert.ToDateTime(_dateTime),
                Quantity = _quantity
            };

            var url = $"{ServiceUrl}ProductsService/UpdateProduct";
            var paramList = new List<ServiceParameterObject>();

            paramList.Add(new ServiceParameterObject("_product", JsonConvert.SerializeObject(products)));

            var apiResult = ApiResult.SendPostRequestFromBody(url, paramList);

            if (apiResult.Status == true)
            {
                return Json(apiResult.Status);

            }
            else
            {
                return Json(false);
            }

        }
        public JsonResult DeleteProduct(int _id)
        {
            var url = $"{ServiceUrl}ProductsService/DeleteProduct";
            var paramList = new List<ServiceParameterObject>();

            paramList.Add(new ServiceParameterObject("_productId", _id.ToString()));

            var apiResult = ApiResult.SendPostRequestFromBody(url, paramList);

            if (apiResult.Status == true)
            {
                return Json(apiResult.Status);

            }
            else
            {
                return Json(false);
            }

        }

        public JsonResult GetProductById(int _id)
        {
            var url = $"{ServiceUrl}ProductsService/GetProductById";
            var paramList = new List<ServiceParameterObject>();

            paramList.Add(new ServiceParameterObject("_productId", _id.ToString()));

            var apiResult = ApiResult.SendPostRequestFromBody(url, paramList);

            if (apiResult.Status == true)
            {
                return Json(apiResult.Message);

            }
            else
            {
                return Json(false);
            }

        }
    }
}
