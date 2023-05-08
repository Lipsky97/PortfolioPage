using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Web.Models
{
    public class PortfolioGridVM
    {
        public string PictureURL { get; set; }
        public string AltText { get; set; }
        public string LinkUrl { get; set; }
        public string LinkText { get; set; }
    }
}