using Portfolio.DB;
using Portfolio.DB.Models;
using Portfolio.Repository.Users.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Repository.PortfolioView
{
    public interface IPortfolioViewRepository
    {

    }
    public class PortfolioViewRepository : IPortfolioViewRepository
    {
        private readonly PortfolioDbContext _db;
        public PortfolioViewRepository(PortfolioDbContext db) 
        {
            _db= db;
        }

        public Result Create(string description, string name, List<byte[]> pics)
        {
            var listOfPictures = new List<PortfolioPicture>();
            foreach(var p in pics)
            {
                var newP = new PortfolioPicture()
                {
                    Sid = Guid.NewGuid().ToString(),
                    Data = p,
                };

                listOfPictures.Add(newP);
            }


            var newPortfolioView = new PortfolioProject
            {
                Description = description,
                Name = name,
                Pictures = listOfPictures,
                Sid = Guid.NewGuid().ToString(),
            };

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

        }
    }
}
