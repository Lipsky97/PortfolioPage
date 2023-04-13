using Portfolio.Repository.Pictures;
using Portfolio.Repository.Pictures.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Service.Pictures
{
    public interface IPicturesService
    {
        string AddPicture(string name, byte[] data);
        PictureVM GetPicture(string sid);
        List<PictureVM> GetAll();
    }
    public class PicturesService : IPicturesService
    {
        private readonly IPicturesRepository _picturesRepository;
        public PicturesService(IPicturesRepository picturesRepository)
        {
            _picturesRepository = picturesRepository;
        }

        public List<PictureVM> GetAll()
        {
            return _picturesRepository.GetAll();
        }

        public PictureVM GetPicture(string sid)
        {
            return _picturesRepository.GetPicture(sid);
        }

        public string AddPicture(string name, byte[] data)
        {
            var newPicture = new PictureVM()
            {
                Name = name,
                Data = data
            };
            return _picturesRepository.AddPicture(newPicture);
        }
    }
}
