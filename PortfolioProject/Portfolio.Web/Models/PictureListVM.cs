using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Web.Models
{
    public class PictureListVM
    {
        public List<PictureVM> PictureList { get; set; }
        public int NofPictures { get; set; }
    }
}