using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNews.Admin.Models
{
    public class Security
    {

        public static void LoginCheck(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Session.GetString("UserName")))
            {
                context.Response.Redirect("/users/Login");
            }
        }

    }
}
