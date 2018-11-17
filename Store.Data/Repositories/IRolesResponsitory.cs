using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{

    public interface IRolesResponsitory : IRepositoryBase<AspNetUserRole>
    {



    }
    public class RolesResponsitory : RepositoryBase<AspNetUserRole>, IRolesResponsitory
    {

        public RolesResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
