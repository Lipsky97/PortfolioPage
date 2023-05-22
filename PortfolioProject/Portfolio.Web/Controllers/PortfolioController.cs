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
            var isAdmin = _usersService.IsAdmin((string)Session["userid"]);
            ViewBag.IsAdmin = isAdmin;
            var data = _portfolioProjectService.GetPortfolioProject(portfolioSid);
            var pictures = new List<PortfolioViewEditPictureVM>();
            foreach (var p in data.Pictures)
            {
                string img = Convert.ToBase64String(p.Data);
                var newPicture = new PortfolioViewEditPictureVM()
                {
                    Sid = p.Sid,
                    Data = img,
                    IsMainPicture = p.IsMainPicture,
                    ProjectId = p.ProjectId,
                    IsVisible = p.IsVisible,
                };
                pictures.Add(newPicture);
            }
            var model = new PortfolioViewVM()
            {
                Description = data.Description,
                Name = data.Name,
                Sid = data.Sid,
                GHLink= data.GHLink,
                HasGHLink = data.HasGHLink,
                Pictures = pictures
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

        [HttpPost]
        public ActionResult UploadPortfolioView(AddPortfolioViewVM vm)
        {
            var pictureList = new List<PortfolioPictureList>();
            foreach (var p in vm.Pictures)
            {
                bool isMainPicture = false;
                if (vm.Pictures.IndexOf(p) == vm.MainPictureIndex - 1)
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
            if (vm.GHLink != null && vm.GHLink != "") 
            {
                vm.HasGHLink = true;
            }
            var result = _portfolioProjectService.Create(vm.Description, vm.Name, vm.HasGHLink, vm.GHLink, pictureList);

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

        public ActionResult PortfolioViewEdit(string projectSid)
        {
            var isAdmin = _usersService.IsAdmin((string)Session["userid"]);
            ViewBag.IsAdmin = isAdmin;
            if (!isAdmin)
            {
                return View("Index");
            }
            var data = _portfolioProjectService.GetPortfolioProject(projectSid);
            var pictures = new List<PortfolioViewEditPictureVM>();
            foreach (var p in data.Pictures)
            {
                string img = Convert.ToBase64String(p.Data);
                var newPicture = new PortfolioViewEditPictureVM()
                {
                    Sid = p.Sid,
                    Data = img,
                    IsMainPicture = p.IsMainPicture,
                    ProjectId = p.ProjectId,
                    IsVisible = p.IsVisible,
                };
                pictures.Add(newPicture);
            }
            var model = new PortfolioViewEditVM()
            {
                Description = data.Description,
                Name = data.Name,
                Sid = data.Sid,
                GHLink = data.GHLink,
                HasGHLink = data.HasGHLink,
                Pictures = pictures
            };
            return View(model);
        }

        public ActionResult AddPictures(List<HttpPostedFileBase> newPictures, string projectSid)
        {
            var pictureList = new List<PortfolioPictureList>();
            foreach (var p in newPictures)
            {
                bool isMainPicture = false;
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
            _portfolioProjectService.AddPictures(pictureList, projectSid);

            return RedirectToAction("PortfolioViewEdit", new { projectSid = projectSid });
        }

        public ActionResult TogglePictureVisibility(string imageSid, string projectSid)
        {
            _portfolioProjectService.ToggleVisible(imageSid);
            return RedirectToAction("PortfolioViewEdit", new { projectSid = projectSid });
        }

        public ActionResult UpdatePortfolio(PortfolioViewVM vm)
        {
            if (vm.GHLink != null && vm.GHLink != "")
            {
                vm.HasGHLink = true;
            }
            else
            {
                vm.HasGHLink = false;
            }

            var project = _portfolioProjectService.GetPortfolioProject(vm.Sid);
            if (vm.GHLink == null)
            {
                project.GHLink = "";
            }
            else
            {
                project.GHLink = vm.GHLink;
            }
            project.HasGHLink = vm.HasGHLink;
            project.Description = vm.Description;
            project.Name = vm.Name;

            _portfolioProjectService.UpdateProject(project);

            return RedirectToAction("PortfolioViewEdit", new { projectSid = vm.Sid });
        }

        public ActionResult DeleteProject(string projectSid)
        {
            _portfolioProjectService.DeleteProject(projectSid);
            return RedirectToAction("Index");
        }

        public ActionResult MakeImageMain(string imageSid, string projectSid)
        {
            _portfolioProjectService.SetMainImage(imageSid, projectSid);

            return RedirectToAction("PortfolioViewEdit", new { projectSid = projectSid });
        }

        public ActionResult DeleteImageEdit(string imageSid, string projectSid)
        {
            _portfolioProjectService.DeletePicture(imageSid);
            
            return RedirectToAction("PortfolioViewEdit", new { projectSid = projectSid });
        }

        private List<PortfolioGridVM> BuildPortfolioGridData(List<PortfolioProject> data)
        {
            var result = new List<PortfolioGridVM>();
            foreach (var p in data)
            {
                var newP = new PortfolioGridVM()
                {
                    PictureURL = Convert.ToBase64String(p.Pictures.First().Data),
                    AltText = "",
                    LinkText = p.Name,
                    LinkUrl = Url.Action("PortfolioView", "Portfolio", new { portfolioSid = $"{p.Sid}"})
                };

                result.Add(newP);
            }
            return result;
        }
    }
}