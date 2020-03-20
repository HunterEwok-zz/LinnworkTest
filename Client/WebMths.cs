using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Client
{
    public class WebMths
    {
        [WebMethod()]
        public static int MakeAPIRequest(int prm)
        {
            int a = 1;
            int b = a + 1;
            b++;
            return prm + b;
        }
    }
}