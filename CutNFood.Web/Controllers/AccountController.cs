using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CutNFood.Web.Models;

namespace CutNFood.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        public AccountController()
        {

        }
        
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            this.Session["IsUserAuthenticated"] = false;
            this.Session["Username"] = "";

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            this.Session["IsUserAuthenticated"] = false;
            this.Session["Username"] = model.Username;

            var account = new CutNFood.Data.Account();
            bool isValidUser = account.AuthenticateUser(model.Username, model.Password);

            if (!isValidUser)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
            var user = account.GetUser(model.Username);
            this.Session["IsUserAuthenticated"] = true;
            this.Session["User"] = user;

            return RedirectToAction("menu", "product");

        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            int usertypeId = 2;
            var account = new CutNFood.Data.Account();
            account.AddUser(2, model.Username, model.Password, model.Email);
            return RedirectToAction("Index", "Home");
         }


        //
        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
       
        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

       }
}