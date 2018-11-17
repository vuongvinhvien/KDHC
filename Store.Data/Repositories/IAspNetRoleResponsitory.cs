using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
  
    public interface IAspNetRoleResponsitory : IRepositoryBase<AspNetRole>
    {



    }
    public class AspNetRoleResponsitory : RepositoryBase<AspNetRole>, IAspNetRoleResponsitory
    {

        public AspNetRoleResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
