using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DB.Models
{
    public class PortfolioPicture
    {
        [Key]
        public string Sid { get; set; }
        public byte[] Data { get; set; }
        public bool IsMainPicture { get; set; }
        public string ProjectId { get; set; }
        public bool IsVisible { get; set; }
    }
}
