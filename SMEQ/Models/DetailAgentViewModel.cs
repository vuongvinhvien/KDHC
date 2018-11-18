using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMEQ.Models
{
    public class DetailAgentViewModel:Store.Data.DataDbContext.AspNetUser
    {
       public int like;
        public int sumconver;
        public DateTime lastonline;
    }
}