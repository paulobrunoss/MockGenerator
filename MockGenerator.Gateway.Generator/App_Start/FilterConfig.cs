using System.Web;
using System.Web.Mvc;

namespace MockGenerator.Gateway.Generator
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
