using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    public interface ISettingResponsitory : IRepositoryBase<Setting>
    {



    }
    public class SettingResponsitory : RepositoryBase<Setting>, ISettingResponsitory
    {

        public SettingResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }

}
