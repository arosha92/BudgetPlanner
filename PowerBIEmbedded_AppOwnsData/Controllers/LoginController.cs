using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerBIEmbedded_AppOwnsData.Models;

namespace PowerBIEmbedded_AppOwnsData.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(PowerBIEmbedded_AppOwnsData.Models.CustomerMaster customerModel)
        {
            using (TestDBTestDBEntities db = new TestDBTestDBEntities())
            {
                var customerDetails = db.CustomerMasters.Where(x => x.Email == customerModel.Email && x.Password == customerModel.Password).FirstOrDefault();
                if (customerDetails == null)
                {
                    customerModel.LoginErrorMessage = "Wrong username or password";
                    return View("Index", customerModel);
                }
                else
                {
                    Session["UserName"] = customerDetails.UserName;
                    Session["Email"] = customerDetails.Email;
                    return RedirectToAction("EmbedReport", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}