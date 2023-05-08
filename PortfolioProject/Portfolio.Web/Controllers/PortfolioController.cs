using Portfolio.DB.Models;
using Portfolio.Repository.PortfolioView.Model;
using Portfolio.Service.PortfolioView;
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
    public class PortfolioController : Controller
    {
        private readonly IPortfolioViewService _portfolioProjectService;
        private readonly IUsersService _usersService;
        public PortfolioController(IPortfolioViewService portfolioProjectService, IUsersService usersService)
        {
            _portfolioProjectService = portfolioProjectService;
            _usersService = usersService;
        }

        public ActionResult Index() 
        {
            var isAdmin = _usersService.IsAdmin((string)Session["userid"]);
            ViewBag.IsAdmin = isAdmin;
            var data = _portfolioProjectService.GetPortfolioProjectList();
            var model = BuildPortfolioGridData(data);
            return View(model); 
        }

        public ActionResult PortfolioView(string portfolioSid) {
            
            var data = _portfolioProjectService.GetPortfolioProject(portfolioSid);
            var pictures = new List<string>();
            foreach (var p in data.Pictures)
            {
                string img = Convert.ToBase64String(p.Data);
                pictures.Add(img);
            }
            var model = new PortfolioViewVM()
            {
                Description = data.Description,
                Name = data.Name,
                Sid = data.Sid,
                PictureURLs = pictures
            };
            return View(model);
        }


        public ActionResult AddPortfolioView()
        {
            var isAdmin = _usersService.IsAdmin((string)Session["userid"]);
            if (!isAdmin)
            {
                return View("Index");
            }

            return View();
        }

        public ActionResult UploadPortfolioView(AddPortfolioViewVM vm)
        {
            var pictureList = new List<PortfolioPictureList>();
            foreach (var p in vm.Pictures)
            {
                bool isMainPicture = false;
                if (vm.Pictures.IndexOf(p) == vm.MainPictureIndex)
                {
                    isMainPicture = true;
                }
                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    p.InputStream.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                    var finalPicutre = new PortfolioPictureList()
                    {
                        File = fileBytes,
                        IsMainPicture = isMainPicture,
                        Name = p.FileName,
                    };

                    pictureList.Add(finalPicutre);
                }

            }
            var result = _portfolioProjectService.Create(vm.Description, vm.Name, pictureList);

            if (result)
            {
                ViewBag.Message = "Sucessfully uploaded new project view";
                return View("AddPortfolioView");
            }
            else
            {
                ViewBag.Message = "Failed to upload new project view";
                return View("AddPortfolioView");
            }
        }

        private List<PortfolioGridVM> BuildPortfolioGridData(List<PortfolioProject> data)
        {
            var result = new List<PortfolioGridVM>();
            foreach (var p in data)
            {
                var newP = new PortfolioGridVM()
                {
                    PictureURL = Convert.ToBase64String(p.Pictures.FirstOrDefault(x => x.IsMainPicture).Data),
                    AltText = "",
                    LinkText = p.Name,
                    LinkUrl = Url.Action("PortfolioView", "Portfolio", new { portfolioSid = $"{p.Sid}"})
                };
            }
            return result;
        }
    }
}