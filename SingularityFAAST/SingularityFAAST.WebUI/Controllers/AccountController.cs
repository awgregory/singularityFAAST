using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SingularityFAAST.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]

        #region [HttpGet] Display Login Page
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        #endregion

        #region [HttpPost] Login Method
        [HttpPost]
        public ActionResult LogIn(UserLogIn user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SingularityDBContext())
                {
                    var username = user.Username;
                    var password = user.Password;

                    var userIsValid = context.UserLogIns.Any(dbUserLogIns => dbUserLogIns.Username == username
                                                                && dbUserLogIns.Password == password);

                    if (userIsValid)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, false);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "username or password is incorrect");
                    }
                }
            }

            return View(user);
        }
        #endregion

        #region Log off method
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }
        #endregion
    }
}