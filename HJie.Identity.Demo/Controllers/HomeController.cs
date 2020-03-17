using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace HJie.Identity.Demo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            var appSettings = System.Web.Configuration.WebConfigurationManager.AppSettings;
            string clientid = appSettings["client_id"];
            string redirecturi = appSettings["redirect_uri"];
            //string responsetype = Config.GetValue("response_type");
            //string serverurl = Config.GetValue("serverurl");
            //string scope = Config.GetValue("sso_scope");
            //string openurl = $"{serverurl}/connect/authorize?client_id={clientid}&redirect_uri={redirecturi}&response_type={responsetype}&scope={scope}&nonce=123456&response_mode=form_post";
            //return Redirect(openurl);
            return View();
        }
        public ActionResult CheckLogin()
        {
            return View();
        }
    }
}