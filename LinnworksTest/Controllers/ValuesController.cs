using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIService.Models;
using System.Data;
using System.IO;
using System.Web;
using System.Text;


namespace APIService.Controllers
{
    public class ValuesController : ApiController
    {
        private const string Authorization = "2986343f-f00a-4bcb-b1d3-95d39f98b804"; //insert Linnworks token here
        private const string ApiLink = "https://eu.linnworks.net/";
        private const string GetCategoriesMethod = "api/Inventory/GetCategories";
        private const string CreateCategoryMethod = "api/Inventory/CreateCategory";
        private const string DeleteCategoryMethod = "api/Inventory/DeleteCategoryById";
        private const string UpdateCategoryMethod = "api/Inventory/UpdateCategory";
        private const string ExecuteCustomScriptQueryMethod = "api/Dashboards/ExecuteCustomScriptQuery";

        public void SendRequest(string api, string method, string type, Dictionary<string, string> parameters, out string response, out WebExceptionStatus webExceptionStatus)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api + method);
            request.Headers.Clear();
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Method = type;
            request.Timeout = 5000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", Authorization);

            if (parameters.Count > 0)
            {
                string postData = "";

                foreach (string key in parameters.Keys)
                {
                    postData += HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(parameters[key]) + "&";
                }

                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.ContentLength = data.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }

            webExceptionStatus = WebExceptionStatus.Success;

            try
            {
                HttpWebResponse objResponse = (HttpWebResponse)request.GetResponse();

                using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream(), Encoding.Default))
                {
                    response = responseStream.ReadToEnd();
                    responseStream.Close();
                }
            }
            catch (WebException we)
            {
                webExceptionStatus = we.Status;
                try
                {
                    if (we.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)we.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                response = reader.ReadToEnd();
                            }
                        }
                    }
                    else response = we.Message;
                }
                catch (Exception e)
                {
                    response = e.Message;
                }
                
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCategories")]
        public dynamic GetCategories()
        {
            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            WebExceptionStatus webExceptionStatus;

            SendRequest(ApiLink, GetCategoriesMethod, "GET", parameters, out response, out webExceptionStatus);

            if (webExceptionStatus != WebExceptionStatus.Success)
            {
                try
                {
                    ErrorMessage msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
                    return msg;
                }
                catch
                {
                    return response;
                }
            }
            else
            {
                Categories categories = new Categories();
                categories.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(response);
                return categories;
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/CreateCategory")]
        public dynamic CreateCategory([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            if (!data.ContainsKey("categoryName"))
                return "Category name require";

            string categoryName = data["categoryName"].ToString();

            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            WebExceptionStatus webExceptionStatus;

            parameters.Add("categoryName", categoryName);

            SendRequest(ApiLink, CreateCategoryMethod, "POST", parameters, out response, out webExceptionStatus);

            if (webExceptionStatus != WebExceptionStatus.Success)
            {
                try
                {
                    ErrorMessage msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
                    return msg;
                }
                catch
                {
                    return response;
                }
            }
            else
            {
                Category category = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(response);
                return category;
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/DeleteCategory")]
        public dynamic DeleteCategory([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            if (!data.ContainsKey("categoryId"))
                return "Category ID require";

            string categoryId = data["categoryId"].ToString();

            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            WebExceptionStatus webExceptionStatus;

            parameters.Add("categoryId", categoryId);

            SendRequest(ApiLink, DeleteCategoryMethod, "POST", parameters, out response, out webExceptionStatus);

            if (webExceptionStatus != WebExceptionStatus.Success)
            {
                try
                {
                    ErrorMessage msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
                    return msg;
                }
                catch
                {
                    return response;
                }
            }
            else
            {
                return "OK";
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/UpdateCategory")]
        public dynamic UpdateCategory([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            if (!data.ContainsKey("category"))
                return "Category require";

            Category category = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(data["category"].ToString());
            if (string.IsNullOrEmpty(category.CategoryName))
                return "Category name require";

            if (string.IsNullOrEmpty(category.CategoryId))
                return "Category ID require";

            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            WebExceptionStatus webExceptionStatus;

            parameters.Add("category", Newtonsoft.Json.JsonConvert.SerializeObject(category));

            SendRequest(ApiLink, UpdateCategoryMethod, "POST", parameters, out response, out webExceptionStatus);

            ErrorMessage msg = new ErrorMessage();

            if (webExceptionStatus != WebExceptionStatus.Success)
            {
                try
                {
                    msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
                    return msg;
                }
                catch
                {
                    return response;
                }
            }
            else
            {
                return "OK";
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ExecuteCustomScriptQuery")]
        public dynamic ExecuteCustomScriptQuery([FromBody]Newtonsoft.Json.Linq.JObject data)
        {
            if (!data.ContainsKey("script"))
                return "Script require";

            string script = data["script"].ToString();

            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            WebExceptionStatus webExceptionStatus;

            parameters.Add("script", script);

            SendRequest(ApiLink, ExecuteCustomScriptQueryMethod, "POST", parameters, out response, out webExceptionStatus);

            ErrorMessage msg = new ErrorMessage();

            if (webExceptionStatus != WebExceptionStatus.Success)
            {
                try
                {
                    msg = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorMessage>(response);
                    return msg;
                }
                catch
                {
                    return response;
                }
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            }
        }
    }
}