using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Web.Models
{
    public class PortfolioViewEditPictureVM
    {
        public string Sid { get; set; }
        public string Data { get; set; }
        public bool IsMainPicture { get; set; }
        public string ProjectId { get; set; }
        public bool IsVisible { get; set; }
    }
}