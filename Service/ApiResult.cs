using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ApiResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }

        public static ApiResult SendPostRequestFromBody(string _url, List<ServiceParameterObject> _params)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(_params), Encoding.UTF8, "application/json");
                var result = client.PostAsync(_url, content).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;

                var apiResult = JsonConvert.DeserializeObject<ApiResult>(resultContent);

                return apiResult;
            }
        }

        public class ServiceParameterObject
        {
            public ServiceParameterObject()
            {

            }

            public ServiceParameterObject(string _k, string _v)
            {
                this.Key = _k;
                this.Value = _v;

            }

            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
