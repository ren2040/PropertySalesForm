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
            return View("ReturnPage");
        }
        
    }
}