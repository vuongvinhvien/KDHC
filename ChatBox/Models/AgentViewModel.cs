using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBox.Models
{
    public class AgentViewModel
    {
       public IEnumerable<AspNetUser> ListAgent;

    }
}