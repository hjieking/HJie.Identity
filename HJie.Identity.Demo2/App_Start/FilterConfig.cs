using System.Web;
using System.Web.Mvc;

namespace HJie.Identity.Demo2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
