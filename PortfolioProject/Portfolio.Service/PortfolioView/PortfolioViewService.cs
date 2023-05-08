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
        bool Create(string description, string name, List<PortfolioPictureList> pictureList);
        PortfolioProject GetPortfolioProject(string sid);
        List<PortfolioProject> GetPortfolioProjectList();
    }
    public class PortfolioViewService : IPortfolioViewService
    {
        private readonly IPortfolioViewRepository _portfolioViewRepository;
        public PortfolioViewService(IPortfolioViewRepository portfolioViewRepository)
        {
            _portfolioViewRepository = portfolioViewRepository;
        }

        public bool Create(string description, string name, List<PortfolioPictureList> pictureList)
        {
            var result = _portfolioViewRepository.Create(description, name, pictureList);
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
    }
}
