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
            ViewBag.Message = "";
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "";
            return View();
        }

        public ActionResult LoginUser(string email, string password)
        {
            var all = _usersService.GetAll();
            string isAuthenticated = _usersService.Authenticate(email, password);
            if (isAuthenticated != null && isAuthenticated != "")
            {
                Session["userid"] = isAuthenticated;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Incorrect E-mail/Passsword";
                return View("Login");
            };
        }

        public ActionResult RegisterUser(string username, string password, string email)
        {
            var result = _usersService.Create(username, password, email);
            if (result)
            {
                ViewBag.Message = "";
                return RedirectToAction("Login");
            } 
            else 
            {
                ViewBag.Message = "User with this mail already exists";
                return View("Register");
            } 
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");
        }
    }
}