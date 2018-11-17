using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
 
    public interface IAspNetUserClaimResponsitory : IRepositoryBase<AspNetUserClaim>
    {



    }
    public class AspNetUserClaimResponsitory : RepositoryBase<AspNetUserClaim>, IAspNetUserClaimResponsitory
    {

        public AspNetUserClaimResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
