using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{

    public interface IAspNetUserLoginResponsitory : IRepositoryBase<AspNetUserLogin>
    {



    }
    public class AspNetUserLoginResponsitory : RepositoryBase<AspNetUserLogin>, IAspNetUserLoginResponsitory
    {

        public AspNetUserLoginResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
