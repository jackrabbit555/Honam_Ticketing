using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Honam_Ticketing.Controllers
{
    public class AccountController : Controller
    {

        TicketingContext db = new TicketingContext();
        private ILoginRepository _loginRepository;
        public AccountController()
        {
            _loginRepository = new LoginService(db);
        }


        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                if (_loginRepository.IsExistUser(login.UserName, login.PassWord))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربری بافت نشد ");
                }
            }

            
            return View(login);
        }

        public ActionResult SignOut() 
        {
        FormsAuthentication.SignOut();
            return Redirect("/");
        
        
        }

    }

    
}