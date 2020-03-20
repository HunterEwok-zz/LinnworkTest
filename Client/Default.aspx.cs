using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;

namespace Client
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private const string ApiLink = "https://localhost:44379/";
        private const string GetCategoriesMethod = "api/GetCategories";
        private const string CreateCategoryMethod = "api/CreateCategory";
        private const string DeleteCategoryMethod = "api/DeleteCategory";
        private const string UpdateCategoryMethod = "api/UpdateCategory";
        private const string ExecuteCustomScriptQueryMethod = "api/ExecuteCustomScriptQuery";

        public static string responseGetCategoriesVal = "";
        public static string responseCreateCategoryVal = "";
        public static string responseDeleteCategoryVal = "";
        public static string responseUpdateCategoryVal = "";
        public static string responseExecuteCustomScriptQueryVal = "";

        private static void SendRequest(string api, string method, string type, Dictionary<string, string> parameters, out string response, out WebExceptionStatus webExceptionStatus)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api + method);
            request.Headers.Clear();
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Method = type;
            request.Timeout = 5000;
            request.ContentType = "application/x-www-form-urlencoded";

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

        [WebMethod]
        public static bool GetCategories()
        {
            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            WebExceptionStatus webExceptionStatus;

            SendRequest(ApiLink, GetCategoriesMethod, "GET", parameters, out response, out webExceptionStatus);
            responseGetCategoriesVal = response;

            return true;
        }

        [WebMethod]
        public static bool CreateCategory(string categoryName)
        {
            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("categoryName", categoryName);
            WebExceptionStatus webExceptionStatus;

            SendRequest(ApiLink, CreateCategoryMethod, "POST", parameters, out response, out webExceptionStatus);
            responseCreateCategoryVal = response;

            return true;
        }

        [WebMethod]
        public static bool DeleteCategory(string categoryId)
        {
            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("categoryId", categoryId);
            WebExceptionStatus webExceptionStatus;

            SendRequest(ApiLink, DeleteCategoryMethod, "POST", parameters, out response, out webExceptionStatus);
            responseDeleteCategoryVal = response;

            return true;
        }

        [WebMethod]
        public static bool UpdateCategory(string categoryId, string categoryName)
        {
            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            Dictionary<string, string> category = new Dictionary<string, string>();
            category.Add("categoryId", categoryId);
            category.Add("categoryName", categoryName);
            
            parameters.Add("category", Newtonsoft.Json.JsonConvert.SerializeObject(category));
            WebExceptionStatus webExceptionStatus;

            SendRequest(ApiLink, UpdateCategoryMethod, "POST", parameters, out response, out webExceptionStatus);
            responseUpdateCategoryVal = response;

            return true;
        }

        [WebMethod]
        public static bool ExecuteCustomScriptQuery(string script)
        {
            string response = "";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("script", script);
            WebExceptionStatus webExceptionStatus;

            SendRequest(ApiLink, ExecuteCustomScriptQueryMethod, "POST", parameters, out response, out webExceptionStatus);
            responseExecuteCustomScriptQueryVal = response;

            return true;
        }
    }
}