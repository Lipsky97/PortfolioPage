using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portfolio.Web.Models
{
    public class AddPortfolioViewVM
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public int MainPictureIndex { get; set; }
        public List<HttpPostedFileBase> Pictures { get; set; }
    }
}