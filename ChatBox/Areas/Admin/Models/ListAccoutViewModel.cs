using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBox.Areas.Admin.Models
{
    public class ListAccoutViewModel
    {
         public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string status { get; set; }
    }
}