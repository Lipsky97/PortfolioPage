using Portfolio.DB.Migrations;
using Portfolio.DB.Models;
using Portfolio.Repository.PortfolioView;
using Portfolio.Repository.PortfolioView.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Service.PortfolioView
{
    public interface IPortfolioViewService
    {
        bool Create(string description, string name, bool hasGHLink, string ghLink, List<PortfolioPictureList> pictureList);
        PortfolioProject GetPortfolioProject(string sid);
        List<PortfolioProject> GetPortfolioProjectList();
        List<PortfolioPicture> GetPortfolioPictures(string projectSid);
        void AddPictures(List<PortfolioPictureList> pictureList, string projectSid);
        void UpdateProject(PortfolioProject portfolioProject);
        void DeleteProject(string portfolioSid);
        void DeletePicture(string pictureSid);
        void SetMainImage(string imageSid, string projectSid);
        void ToggleVisible(string imageSid);
    }
    public class PortfolioViewService : IPortfolioViewService
    {
        private readonly IPortfolioViewRepository _portfolioViewRepository;
        public PortfolioViewService(IPortfolioViewRepository portfolioViewRepository)
        {
            _portfolioViewRepository = portfolioViewRepository;
        }

        public bool Create(string description, string name, bool hasGHLink, string ghLink, List<PortfolioPictureList> pictureList)
        {
            var result = _portfolioViewRepository.Create(description, name, hasGHLink, ghLink, pictureList);
            return result.IsSuccess;
        }

        public PortfolioProject GetPortfolioProject(string sid)
        {
            var result = _portfolioViewRepository.GetPortfolioProject(sid);
            return result;
        }

        public List<PortfolioProject> GetPortfolioProjectList()
        {
            var result = _portfolioViewRepository.GetPortfolioProjectList();
            if (result == null)
            {
                result = new List<PortfolioProject>();
            }
            return result;
        }

        public List<PortfolioPicture> GetPortfolioPictures(string projectSid)
        {
            return _portfolioViewRepository.GetPortfolioPictures(projectSid);
        }

        public void AddPictures(List<PortfolioPictureList> pictureList, string projectSid)
        {
            _portfolioViewRepository.AddPictures(pictureList, projectSid);
        }

        public void ToggleVisible(string imageSid)
        {
            _portfolioViewRepository.ToggleVisible(imageSid);
        }

        public void UpdateProject(PortfolioProject portfolioProject) 
        {
            _portfolioViewRepository.UpdateProject(portfolioProject);
        }

        public void SetMainImage(string imageSid, string projectSid)
        {
            _portfolioViewRepository.SetMainImage(imageSid, projectSid);
        }

        public void DeleteProject(string portfolioSid)
        {
            _portfolioViewRepository.DeleteProject(portfolioSid);
        }

        public void DeletePicture(string pictureSid) 
        {
            _portfolioViewRepository.DeletePicture(pictureSid);
        }
    }
}
