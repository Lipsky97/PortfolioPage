using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Service.Users.Model
{
    public class UserVD
    {
        public Guid Sid { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public bool IsActive { get; set; }
    }
}
