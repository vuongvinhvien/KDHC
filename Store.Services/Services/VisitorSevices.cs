using Microsoft.AspNet.Identity;
using Store.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.DataDbContext;

namespace Store.Sevices.Services
{
    public interface IVisitorSevices
    {
        Visitor GetByID(string id);

    }
    public class VisitorSevices : IVisitorSevices
    {
        private readonly IVisitorResponsitory _Visitor;
       
        public VisitorSevices(IVisitorResponsitory Visitor)
        {
            _Visitor = Visitor;
        }
        public Visitor GetByID(string id)
        {
            return _Visitor.GetById(id);
        }
    }
}
