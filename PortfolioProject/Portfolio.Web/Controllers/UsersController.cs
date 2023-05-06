using Portfolio.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public ActionResult Settings()
        {
            ViewBag.Message = "";
            if ((string)Session["userid"] == null || (string)Session["userid"] == "")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ChangeAccount(string newUsername, string newPassword, string newEmail)
        {
            var result = _usersService.Update((string)Session["userid"], newUsername, newPassword, newEmail);
            if (result.IsSuccess)
            {
                ViewBag.Message = "Succesfully changed account details";
            }
            else
            {
                ViewBag.Message = "Failed to change account details";
            }

            return View();
        }
    }
}