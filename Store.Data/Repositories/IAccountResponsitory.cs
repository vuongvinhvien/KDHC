using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    public interface IAccountResponsitory : IRepositoryBase<AspNetUser>
    {



    }
    public class AccountResponsitory : RepositoryBase<AspNetUser>, IAccountResponsitory
    {

        public AccountResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }

}
