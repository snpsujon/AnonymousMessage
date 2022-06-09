using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnonymousMessage.Models
{
    public class Users
    {
        [Key]
        public int UserId{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }

    }
}
