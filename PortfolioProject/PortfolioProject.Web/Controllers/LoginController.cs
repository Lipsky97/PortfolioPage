using Portfolio.Service.Users;
using Portfolio.Service.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioProject.Web.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : Controller
    {
        public readonly IUsersService _usersService;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(UserVD user)
        {
            string msg = string.Empty;
            try
            {
                user.Sid = new Guid();
                var result = _usersService.Create(user.UserName, user.Password, user.Email);
                msg = result.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return Json(msg);
        }
    }
}