using Portfolio.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Web.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioProjectService _portfolioProjectService;
        public PortfolioController(IPortfolioProjectService portfolioProjectService)
        {
            _portfolioProjectService = portfolioProjectService;
        }

        public ActionResult Index() 
        {
            var data = GetPortfolioGridData();
            return View(data); 
        }

        public ActionResult PortfolioView(string portfolioSid) {
            var data = _portfolioProjectService.GetPortfolioProject(portfolioSid);
            var pictures = new List<string>();
            foreach (var p in data.Pictures)
            {
                string img = Convert.ToBase64String(p);
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

        private List<PortfolioGridVM> GetPortfolioGridData()
        {
            var result = new List<PortfolioGridVM>();
            var newP = new PortfolioGridVM()
            {
                PictureURL = Url.Content("~/App_Images/ImageGrid/548651_325519860836288_650402660_n.jpg"),
                AltText = "Hyhy",
                LinkText = "Przykładowy tytuł projektu",
                LinkUrl = Url.Action("CV")
            };
            result.Add(newP);
            result.Add(newP);
            result.Add(newP);
            result.Add(newP);
            result.Add(newP);
            return result;
        }
    }
}