using AutoMapper;
using SMEQ.Models;
using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace SMEQ.Mapping
{
    public class Mapping : Profile
    { 
        public override string ProfileName
        {
            get { return "Mapping"; }
        }
     
        public Mapping()
        {

            CreateMap<Customer, HistoryViewModel>();
            CreateMap<AspNetUser, SMEQ.Areas.Admin.Models.ListAccoutViewModel>();
            CreateMap<ChatLine, ChatLineViewModel>();
            CreateMap<AspNetUser, DetailAgentViewModel>();
        }
    }
}