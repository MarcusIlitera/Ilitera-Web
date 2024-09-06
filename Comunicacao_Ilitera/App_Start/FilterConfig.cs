using System.Web;
using System.Web.Mvc;

namespace Comunicacao_Ilitera
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
