using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
   
    public interface IVisitorResponsitory : IRepositoryBase<Visitor>
    {

    }
    public class VisitorResponsitory : RepositoryBase<Visitor>, IVisitorResponsitory
    {

        public VisitorResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
