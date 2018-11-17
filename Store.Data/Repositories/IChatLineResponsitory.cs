using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{

    public interface IChatLineResponsitory : IRepositoryBase<ChatLine>
    {



    }
    public class ChatLineResponsitory : RepositoryBase<ChatLine>, IChatLineResponsitory
    {

        public ChatLineResponsitory(IUnitOfWork _Db) : base(_Db)
        {

        }

    }
}
