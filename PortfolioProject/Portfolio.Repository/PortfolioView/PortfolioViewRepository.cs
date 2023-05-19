using Microsoft.EntityFrameworkCore;
using Portfolio.DB;
using Portfolio.DB.Models;
using Portfolio.Repository.PortfolioView.Model;
using Portfolio.Repository.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Repository.PortfolioView
{
    public interface IPortfolioViewRepository
    {
        Result Create(string description, string name, bool hasGHLink, string ghLink, List<PortfolioPictureList> pics);
        PortfolioProject GetPortfolioProject(string sid);
        List<PortfolioProject> GetPortfolioProjectList();
        List<PortfolioPicture> GetPortfolioPictures(string portfolioSid);
        void AddPictures(List<PortfolioPictureList> pictureList, string projectSid);
        void UpdateProject(PortfolioProject project);
        void DeleteProject(string portfolioSid);
        void DeletePicture(string pictureSidList);
        void SetMainImage(string imageSid, string projectSid);
    }
    public class PortfolioViewRepository : IPortfolioViewRepository
    {
        private readonly PortfolioDbContext _db;
        public PortfolioViewRepository(PortfolioDbContext db) 
        {
            _db= db;
        }

        public Result Create(string description, string name, bool hasGHLink, string ghLink, List<PortfolioPictureList> pics)
        {
            var listOfPictures = new List<PortfolioPicture>();
            var newPortfolioView = new PortfolioProject
            {
                Description = description,
                Name = name,
                Pictures = listOfPictures,
                HasGHLink = hasGHLink,
                IsActive = true,
                GHLink = ghLink,
                Sid = Guid.NewGuid().ToString(),
            };

            foreach (var p in pics)
            {
                var newP = new PortfolioPicture()
                {
                    Sid = Guid.NewGuid().ToString(),
                    Data = p.File,
                    ProjectId = newPortfolioView.Sid,
                    IsMainPicture = p.IsMainPicture
                };

                newPortfolioView.Pictures.Add(newP);
            }

            try
            {
                _db.Projects.Add(newPortfolioView);
                _db.SaveChanges();
                return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Result() { IsSuccess = false, Message = $"Failed to add new View: {ex.Message}" };
            }
        }

        public PortfolioProject GetPortfolioProject(string sid)
        {
            try
            {
                var project = _db.Projects.Include(x => x.Pictures).FirstOrDefault(p => p.Sid == sid);
                if (project == null)
                {
                    return null;
                }
                return project;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PortfolioProject> GetPortfolioProjectList()
        {
            try
            {
                var projectList = _db.Projects.Where(x => x.IsActive).ToList();
                var mainPictures = _db.PortfolioPictures.Where(x => x.IsMainPicture);

                var result = BuildPortfolioProjectObj(projectList, mainPictures);

                if (projectList.Count <= 0)
                {
                    return null;
                }
                return projectList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PortfolioPicture> GetPortfolioPictures(string portfolioSid)
        {
            return _db.PortfolioPictures.Where(x => x.ProjectId== portfolioSid).ToList();
        }

        public void AddPictures(List<PortfolioPictureList> pictureList, string projectSid)
        {
            var project = _db.Projects.Include(x => x.Pictures).FirstOrDefault(x => x.Sid == projectSid);
            foreach (var picture in pictureList)
            {
                var newP = new PortfolioPicture()
                {
                    Sid = Guid.NewGuid().ToString(),
                    Data = picture.File,
                    ProjectId = project.Sid,
                    IsMainPicture = picture.IsMainPicture
                };

                project.Pictures.Add(newP);
            }
            try
            {
                _db.Projects.Update(project);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateProject(PortfolioProject project)
        {
            try
            {
                _db.Projects.Update(project);
                _db.SaveChanges();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public void SetMainImage(string imageSid, string projectSid)
        {
            var images = GetPortfolioPictures(projectSid);
            foreach(var image in images)
            {
                image.IsMainPicture = false;
            }
            var newMain = _db.PortfolioPictures.FirstOrDefault(x => x.Sid == imageSid);
            newMain.IsMainPicture = true;
            _db.SaveChanges();
        }

        public void DeleteProject(string portfolioSid)
        {
            var project =_db.Projects.FirstOrDefault(x => x.Sid == portfolioSid);
            if (project == null) return;
            project.IsActive = false;
            _db.Projects.Update(project);
            _db.SaveChanges();
        }

        public void DeletePicture(string pictureSid)
        {
            var picture = _db.PortfolioPictures.FirstOrDefault(x => x.Sid == pictureSid);
            if (picture.IsMainPicture)
            {
                var newMainPicture = _db.PortfolioPictures.FirstOrDefault(x => x.ProjectId == picture.ProjectId && x.Sid != picture.Sid);
                if (newMainPicture == null) 
                {
                    throw new Exception("Cannot remove the only project picture");
                }
                newMainPicture.IsMainPicture = true;
                _db.PortfolioPictures.Update(newMainPicture);
            }
            if (picture == null) return;
            try
            {
                _db.PortfolioPictures.Remove(picture);
                _db.SaveChanges();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        private List<PortfolioProject> BuildPortfolioProjectObj(List<PortfolioProject> projectList, IQueryable<PortfolioPicture> portfolioPictures)
        {
            foreach(var project in projectList)
            {
                var mainPciture = new List<PortfolioPicture>();
                mainPciture.Add(portfolioPictures.FirstOrDefault(x => x.ProjectId == project.Sid));
                project.Pictures = mainPciture;
            }

            return projectList;
        }
    }
}
