using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DB.Models
{
    public class CV
    {
        [Key]
        public string SID { get; set; }
        public double Version { get; set; }
        public byte[] File { get; set; }
    }
}
