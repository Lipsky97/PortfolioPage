using Portfolio.Service.Pictures;
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

        public HomeController(IPicturesService picturesService)
        {
            _picturesService = picturesService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
        {
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
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}