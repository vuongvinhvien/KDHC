using AutoMapper;
using ChatBox.Models;
using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace ChatBox.Mapping
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
            CreateMap<AspNetUser, ChatBox.Areas.Admin.Models.ListAccoutViewModel>();
            CreateMap<ChatLine, ChatLineViewModel>();
            CreateMap<AspNetUser, DetailAgentViewModel>();
        }
    }
}