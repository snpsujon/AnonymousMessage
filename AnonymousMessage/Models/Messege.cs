using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnonymousMessage.Models
{
    public class Messege
    {
        public int Id { get; set; }
        public string msg { get; set; }
        public string city { get; set; }
        public string username { get; set; }

        public string ip { get; set; }
    }
}
