using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HJie.Identity.Demo2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var appSettings = System.Web.Configuration.WebConfigurationManager.AppSettings;
            string clientid = appSettings["client_id"];
            string redirecturi = appSettings["redirect_uri"];
            string responsetype = appSettings["response_type"];
            string serverurl = appSettings["serverurl"];
            string scope = appSettings["sso_scope"];
            string openurl = $"{serverurl}/connect/authorize?client_id={clientid}&redirect_uri={redirecturi}&response_type={responsetype}&scope={scope}&nonce=123456&response_mode=form_post";
            return Redirect(openurl);
        }
        public ActionResult CheckLogin()
        {
            var _idToken = Request.Form["id_token"];
            var _accessToken = Request.Form["access_token"];
            var idTokenList = _idToken.Split('.');
            JObject myClaim = JObject.Parse(StringFromBase64Url(idTokenList[1]));
            string sub = myClaim["sub"].ToString();
            string identity = Request.Params["identity"];//机构ID
            //需要验证成功即可

            return Redirect("~/Home/Index");
        }

        private static string StringFromBase64Url(string base64Url)
        {
            string padded = base64Url.Length % 4 == 0
                ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
            string base64 = padded.Replace("_", "/")
                                  .Replace("-", "+");
            return Base64Decode(base64);
        }
        public static string Base64Decode(string result)
        {
            return Base64Decode(Encoding.UTF8, result);
        }
        public static string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}