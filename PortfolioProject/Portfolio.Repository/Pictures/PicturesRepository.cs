using Portfolio.DB;
using Portfolio.DB.Models;
using Portfolio.Repository.Pictures.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Repository.Pictures
{
    public interface IPicturesRepository
    {
        List<PictureVM> GetAll();
        PictureVM GetPicture(string sid);
        string AddPicture(PictureVM picture);

    }
    public class PicturesRepository : IPicturesRepository
    {
        private readonly PortfolioDbContext _db;
        public PicturesRepository(PortfolioDbContext db)
        {
            _db = db;
        }

        public List<PictureVM> GetAll()
        {
            var pictures = _db.Pictures.Where(x => x.Sid != null).ToList();
            var result = new List<PictureVM>();
            foreach (var picture in pictures)
            {
                var pictureVM = new PictureVM()
                {
                    Name= picture.Name,
                    Data= picture.Data,
                };
                result.Add(pictureVM);
            }

            return result;
        }

        public PictureVM GetPicture(string sid)
        {
            var picture = _db.Pictures.FirstOrDefault(x => x.Sid == sid);
            var result = new PictureVM()
            {
                Data= picture.Data,
                Name = picture.Name,
            };
            return result;
        }

        public string AddPicture(PictureVM picture)
        {
            var newPicture = new Picture()
            {
                Sid = Guid.NewGuid().ToString(),
                Name = picture.Name,
                Data = picture.Data
            };
            _db.Pictures.Add(newPicture);
            _db.SaveChanges();
            return newPicture.Name;
        }
    }
}
