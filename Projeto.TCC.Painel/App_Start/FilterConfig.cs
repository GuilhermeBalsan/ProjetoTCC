using System.Web;
using System.Web.Mvc;

namespace Projeto.TCC.Painel
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
