using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBox.Models
{
 
    public partial class ContentChatHepperHistory
    {

        public string Visitor { get; set; }

        public string Agent { get; set; }

        public string VisitorName { get; set; }
        public string AgentName { get; set; }

        public bool? feeling { get; set; }

        public string Comment { get; set; }

        public string Address { get; set; }

        public DateTime? Date { get; set; }

        public string sumary { get; set; }
    }
    public class HistoryViewModel
    {
        public string ID_DashBoard { get; set; }
        public string ID_User { get; set; }
        public IEnumerable<AspNetUser> ListAgent { get; set; }
        public IEnumerable<ContentChatHepperHistory> History { get; set; }

    }
}