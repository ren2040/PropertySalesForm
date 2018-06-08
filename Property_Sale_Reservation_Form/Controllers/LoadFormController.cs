using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Property_Sale_Reservation_Form.Controllers
{
    public class LoadFormController : Controller
    {
        // GET: LoadForm
        public ActionResult Index(int reference)
        {

            string url = HttpContext.Request.Url.AbsolutePath;
            ViewBag.ReferenceURL = url.Substring(16,5).ToString();
            DBAccess dbAccess = new DBAccess();
            List<Models.FormData> formDetails = dbAccess.getForm(reference);

            ViewBag.addressLine1 = formDetails[0].addressLine1;
            ViewBag.addressLine2 = formDetails[0].addressLine2;
            ViewBag.city = formDetails[0].city;
            ViewBag.state = formDetails[0].state;
            ViewBag.zip = formDetails[0].zip;
            ViewBag.firstName = formDetails[0].firstName;
            ViewBag.lastNane = formDetails[0].lastName;
            ViewBag.amount = formDetails[0].amount;

            return View();
        }


        public ActionResult ReturnPage()
        {
            string reference = Request.QueryString["Ref"];
            string ServiceProcessed = Request.QueryString["serviceprocessed"];
            string receipt = Request.QueryString["receiptnumber"];

            DBAccess dbAccess = new DBAccess();
            List<Models.FormData> formDetails = dbAccess.getForm(Convert.ToInt32(reference));

            if (ServiceProcessed == "true")
            {

                EmailUtils.SendEmail("Hackney Sales Payment Receipt, Receipt number: "
                                     + receipt, "This is the payment receipt for the following property..."
                                                + System.Environment.NewLine + System.Environment.NewLine +
                                                System.Environment.NewLine +
                                                "Address Line 1:" + formDetails[0].addressLine1 + System.Environment.NewLine
                                                + "Address Line 2:" + formDetails[0].addressLine2 + System.Environment.NewLine
                                                + "City:" + formDetails[0].city + System.Environment.NewLine
                                                + "State/Province/Region:" + formDetails[0].state + System.Environment.NewLine
                                                + "Zip/Postal Code:" + formDetails[0].zip + System.Environment.NewLine
                                                + System.Environment.NewLine + System.Environment.NewLine
                                                + "Amount Paid:" + formDetails[0].amount
                                                + "Payment was sucessful");

                return View("ReturnPage");
            }else{

                EmailUtils.SendEmail("Hackney Sales Payment Receipt, Receipt number: "
                                     + receipt, "PAYMENT FAILED" + System.Environment.NewLine + "This is the payment receipt for the following property..."
                                                + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine +
                                               
                                                "Address Line 1:" + formDetails[0].addressLine1 + System.Environment.NewLine
                                                + "Address Line 2:" + formDetails[0].addressLine2 + System.Environment.NewLine
                                                + "City:" + formDetails[0].city + System.Environment.NewLine
                                                + "State/Province/Region:" + formDetails[0].state + System.Environment.NewLine
                                                + "Zip/Postal Code:" + formDetails[0].zip + System.Environment.NewLine
                                                + System.Environment.NewLine + System.Environment.NewLine
                                                
                                                + "----------PAYMENT FAILED----------"
                                                + System.Environment.NewLine + "Please contact the customer.");
                return View("FailedPage");
            }

        }
        public ActionResult Pay()
        {


            return RedirectToAction("Pay", "Home");
        }

    }
}