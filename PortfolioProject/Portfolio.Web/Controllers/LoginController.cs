using Microsoft.Ajax.Utilities;
using Portfolio.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersService _usersService;
        
        public LoginController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LoginUser(string username, string password)
        {
            var all = _usersService.GetAll();
            string isAuthenticated = _usersService.Authenticate(username, password);
            if (isAuthenticated != null || isAuthenticated != "")
            {
                Session["userid"] = isAuthenticated;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Register", "Login");
            };
        }

        public ActionResult RegisterUser(string username, string password, string email)
        {
            var result = _usersService.Create(username, password, email);
            if (result) return RedirectToAction("Login");
            else return Json(result);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");
        }
    }
}