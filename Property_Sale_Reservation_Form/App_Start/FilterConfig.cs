using System.Web;
using System.Web.Mvc;

namespace Property_Sale_Reservation_Form
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
