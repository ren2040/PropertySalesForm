using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Property_Sale_Reservation_Form.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public ActionResult Index(string AddressLine1Prop, string AddressLine2Prop, string CityProp,
            string StateProvinceRegionProp,
            string ZipPostalCodeProp, string AmountProp , string FirstName, string LastName
        )
        {
            
       
            ViewBag.Stage2 = "true";

            DBAccess dbAccess = new DBAccess();
            dbAccess.RecordForm(AddressLine1Prop, AddressLine2Prop, CityProp, StateProvinceRegionProp, ZipPostalCodeProp,
                                AmountProp, FirstName, LastName, ViewBag.Stage2);

            return View();

        }

        [HttpGet]
        public ActionResult Pay()
        {
           
            return Redirect(
                "https://hackney.paris-epayments.co.uk/paymentstest/sales/launchinternet.aspx?returnurl=http://lbhcrmappd01:802/Licensing/Thankyou.aspx?CRMData=CAS-775515-M9F2QZ$3cb039e7-f421-e811-a4ef-005056986-64$10.00&payforbasketmode=true&returntext=BacktoSomething&ignoreconfirmation=true&data=Keepthisandreturnitattheend&recordxml=<records><record><reference>006884</reference><fund>02</fund><amount>10.00</amount><text></text></record></records>");
        }
        public ActionResult Index()
       {
            ViewBag.Stage2 = "false";
           return View();
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

        public void DisableButton()
        {

        }
    }
}