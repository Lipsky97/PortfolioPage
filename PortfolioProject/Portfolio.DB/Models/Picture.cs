using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DB.Models
{
    public class Picture
    {
        [Key]
        public string Sid { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
