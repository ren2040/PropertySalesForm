using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Property_Sale_Reservation_Form.Models;

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


            PageDetails details = new PageDetails();
            details.stage2 = "true";


            DBAccess dbAccess = new DBAccess();
            int reference = dbAccess.RecordForm(AddressLine1Prop, AddressLine2Prop, CityProp, StateProvinceRegionProp, ZipPostalCodeProp,
                                AmountProp, FirstName, LastName, details.stage2);
          
            
            
            ViewBag.URL = "https://myhackney.hackney.gov.uk/propertysales/LoadForm/Index/" + reference ;

            //SendEmail sendEmail = new SendEmail();
            //sendEmail.sendEmail(reference, ViewBag.URL);

            EmailUtils.SendEmail("Property Sales Reservation Form :" + reference , "This is an automated email " +
                                                                                   "for the Property Sales Reservation form created for property " +
                                                                                   AddressLine1Prop + "," + AddressLine2Prop + "," + CityProp + ","
                                                                                   + StateProvinceRegionProp + "," + ZipPostalCodeProp + ". Please forward " +
                                                                                   "The following link to the customer to confirm form details and make the payment..."
                                                                                   + ViewBag.URL);



            return View(details);

        }

        [HttpGet]
        public ActionResult Pay(string reference)
        {

           string url = HttpContext.Request.Url.AbsolutePath;
            DBAccess dbAcess = new DBAccess();

            int referenceInt = Convert.ToInt32(reference);
            
            List<double> amount = dbAcess.getAmount(referenceInt);
            string amount2 = amount[0].ToString();
          string returnURL = "https://hackney.paris-epayments.co.uk/paymentstest/sales/launchinternet.aspx?returnurl=https://myhackney.hackney.gov.uk/propertysales/LoadForm/ReturnPage?Ref=" + reference + "&payforbasketmode=true&returntext=Back&ignoreconfirmation=true&data=Keepthisandreturnitattheend&recordxml=<records><record><reference>006884</reference><fund>02</fund><amount>" + amount2 +"</amount><text></text></record></records>";
          return Redirect(returnURL);
        }
        public ActionResult Index()
        {
            PageDetails dets = new PageDetails();
            dets.stage2 = "false";
            
           return View(dets);
      }
        

    
    }
}