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
    public class ProductsServiceController : ControllerBase
    {
        [HttpPost("GetProductsList")]
        public string GetProductsList()
        {
            try
            {
                var productsDb = ProductsDb.GetInstance();
                var result = productsDb.GetProductsList();
                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != null ? "Success" : "Error",
                    Message = result != null ? JsonConvert.SerializeObject(result) : "",
                    Status = result != null

                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("AddNewProduct")]
        public string AddNewProduct([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Products>(_data.FirstOrDefault(x => x.Key == "_product").Value);

                var productsDb = ProductsDb.GetInstance();
                var result = productsDb.AddNewProduct(product);
                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != null ? "Success" : "Error",
                    Message = result != null ? JsonConvert.SerializeObject(result) : "",
                    Status = result != null

                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost("UpdateProduct")]
        public String UpdateProduct([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Products>(_data.FirstOrDefault(x => x.Key == "_product").Value);

                var productDb = ProductsDb.GetInstance();
                var result = productDb.UpdateProducts(product);

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != false ? "Success" : "Error",
                    Message = result != false ? JsonConvert.SerializeObject(result) : "",
                    Status = result != false
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

        [HttpPost("DeleteProduct")]
        public String DeleteProduct([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var productId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_productId").Value);

                var productDb = ProductsDb.GetInstance();
                var result = productDb.DeleteProduct(productId);

                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != false ? "Success" : "Error",
                    Message = result != false ? JsonConvert.SerializeObject(result) : "",
                    Status = result != false
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

        [HttpPost("GetProductById")]
        public string GetProductById([FromBody] List<ServiceParameterObject> _data)
        {
            try
            {
                var productId = Convert.ToInt32(_data.FirstOrDefault(x => x.Key == "_productId").Value);

                var productsDb = ProductsDb.GetInstance();
                var result = productsDb.GetProductById(productId);
                return JsonConvert.SerializeObject(new ApiResult()
                {
                    Response = result != null ? "Success" : "Error",
                    Message = result != null ? JsonConvert.SerializeObject(result) : "",
                    Status = result != null

                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
