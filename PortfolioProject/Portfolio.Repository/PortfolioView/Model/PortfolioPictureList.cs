using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Repository.PortfolioView.Model
{
    public class PortfolioPictureList
    {
        public string Sid { get; set; }
        public byte[] File { get; set; }
        public string Name { get; set; }
        public bool IsMainPicture { get; set; }
        public bool IsVisible { get; set; }
    }
}
