using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBox.Models
{
 
    public partial class ChatLineViewModel
    {

        public string Visitor { get; set; }
        public string Agent { get; set; }
        public bool? Actor { get; set; }

        public string Line { get; set; }

        public DateTime? Date { get; set; }


        public string NameAgent { get; set; }

    }
}