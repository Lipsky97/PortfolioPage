using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Web.Models
{
    public class PortfolioViewVM
    {
        public string Sid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GHLink { get; set; }
        public bool HasGHLink { get; set; }
        public List<string> PictureURLs { get; set; }
    }
}