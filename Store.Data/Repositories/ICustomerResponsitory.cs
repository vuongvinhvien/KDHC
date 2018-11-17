using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{

    public interface ICustomerResponsitory : IRepositoryBase<Customer>
    {

    }
    public class CustomerResponsitory : RepositoryBase<Customer>, ICustomerResponsitory
    {

        public CustomerResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
