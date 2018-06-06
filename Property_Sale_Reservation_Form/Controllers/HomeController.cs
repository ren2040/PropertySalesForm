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
            int reference = dbAccess.RecordForm(AddressLine1Prop, AddressLine2Prop, CityProp, StateProvinceRegionProp, ZipPostalCodeProp,
                                AmountProp, FirstName, LastName, ViewBag.Stage2);
            ViewBag.AmountToBePaid = AmountProp;
            ViewBag.URL = "http://localhost:57764/LoadForm/Index/" + reference ;

            //SendEmail sendEmail = new SendEmail();
            //sendEmail.sendEmail(reference, ViewBag.URL);

            EmailUtils.SendEmail("Property Sales Reservation Form :" + reference , "This is an automated email " +
                                                                                   "for the Property Sales Reservation form created for property: " +
                                                                                   AddressLine1Prop + "," + AddressLine2Prop + "," + CityProp + ","
                                                                                   + StateProvinceRegionProp + "," + ZipPostalCodeProp + ". Please forward " +
                                                                                   "The following link to the customer to confirm form details and make the payment..."
                                                                                   + ViewBag.URL);



            return View();

        }

        [HttpGet]
        public ActionResult Pay()
        {
            //http://localhost:57764/LoadForm/ReturnPage
            string returnURL = "https://hackney.paris-epayments.co.uk/paymentstest/sales/launchinternet.aspx?returnurl=http://localhost:57764/LoadForm/ReturnPage?CRMData=CAS-775515-M9F2QZ$3cb039e7-f421-e811-a4ef-005056986-64$10.00&payforbasketmode=true&returntext=BacktoSomething&ignoreconfirmation=true&data=Keepthisandreturnitattheend&recordxml=<records><record><reference>006884</reference><fund>02</fund><amount>10.00</amount><text></text></record></records>";
            return Redirect(returnURL);
        }
        public ActionResult Index()
       {
            ViewBag.Stage2 = "false";
           return View();
      }
        

     
    }
}