using Portfolio.Repository.CVs.Model;
using Portfolio.Service.CVs;
using Portfolio.Service.Pictures;
using Portfolio.Service.Users;
using Portfolio.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPicturesService _picturesService;
        private readonly IUsersService _usersService;
        private readonly ICVsService _cvsService;

        public HomeController(IPicturesService picturesService, IUsersService usersService, ICVsService cVsService)
        {
            _picturesService = picturesService;
            _usersService = usersService;
            _cvsService = cVsService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            ViewBag.isAdmin = _usersService.IsAdmin((string)Session["userid"]);
            ViewBag.Message = "Your application description page.";
            var pictures = _picturesService.GetAll();
            var result = new List<PictureVM>();
            foreach (var p in pictures)
            {
                var newPicture = new PictureVM()
                {
                    Data = p.Data,
                    Name= p.Name,
                };

                result.Add(newPicture);
            }

            return View(new PictureListVM() { PictureList = result, NofPictures = pictures.Count });
        }

        public ActionResult AddPicture(HttpPostedFileBase data, string name)
        {
            byte[] pictureData = null;
            using (var binaryReader = new BinaryReader(data.InputStream))
            {
                pictureData = binaryReader.ReadBytes(data.ContentLength);
            }
            var result = _picturesService.AddPicture(name, pictureData);
            return Json(true);
        }

        public ActionResult CV()
        {
            ViewBag.IsAdmin = _usersService.IsAdmin((string)Session["userid"]);
            return View();
        }

        public ActionResult UploadCV(HttpPostedFileBase data)
        {
            if (data != null && data.ContentLength > 0 && data.ContentType == "application/pdf")
            {
                try
                {
                    byte[] file;
                    using (var br = new BinaryReader(data.InputStream))
                    {
                        file = br.ReadBytes(data.ContentLength);
                        var cv = new CVVM
                        {
                            File = file,
                        };
                        _cvsService.AddCV(cv);
                        return View("CV");
                    }
                }
                catch (Exception ex) 
                {
                    ViewBag.Message = "Error: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Please select a PDF file first";
            }

            return View();
        }

        public ActionResult DownloadLatestCV()
        {
            var cv = _cvsService.GetLatestCV();
            return File(cv.File, "application/pdf", "Oskar_Lipienski_CV.pdf");
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}