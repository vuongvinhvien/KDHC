using Microsoft.AspNet.Identity;
using Store.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.DataDbContext;
using System.Data.SqlClient;

namespace Store.Sevices.Services
{
    public interface IChatLineSevices
    {
        IEnumerable<ChatLine> GetChatLineByUser(string id);
        IEnumerable<ChatLine> GetByVisitorAgent(string id, string idAdmin);

        IEnumerable<DateTime> getlasttimeonline(string idagent);
        IEnumerable<ChatLine> FitterMutilt(string das, string mess, string visitor, string email, string phone);

    }
    public class ChatLineSevices : IChatLineSevices
    {
        private readonly IChatLineResponsitory _ChatLine;
        private readonly IAccountResponsitory _Account;
        private readonly IStoreProduce _StoreProduce;
        public ChatLineSevices(IChatLineResponsitory ChatLine, IAccountResponsitory Account, IStoreProduce StoreProduce)
        {
            _ChatLine = ChatLine;
            _Account = Account;
            _StoreProduce = StoreProduce;
        }
        public IEnumerable<ChatLine> GetChatLineByUser(string id)
        {

            var account = _Account.GetById(id);
            if (account.IsMain == true)
            {
                return _ChatLine.GetAll().Where(x => x.ID_Customer == account.ID_Customer);
            }
            else
            {
                IEnumerable<SqlParameter> Params = new List<SqlParameter> {
                   new SqlParameter { ParameterName = "@idcus", Value = account.ID_Customer },
                  new SqlParameter { ParameterName = "@idagent", Value = id }
                            };

                var x = _StoreProduce.Run<ChatLine>(new StoreProduceModel { NameProduce = "SelectChatLine", Params = Params });
                return x;
            }

        }
        public IEnumerable<ChatLine> GetByVisitorAgent(string id, string idAdmin)
        {
            var Acc = _Account.GetById(idAdmin);
            return _ChatLine.GetAll().Where(x => x.Visitor == id && x.ID_Customer == Acc.ID_Customer);
        }
        public IEnumerable<ChatLine> FitterMutilt(string das, string mess, string visitor, string email, string phone)
        {
            IEnumerable<SqlParameter> Params = new List<SqlParameter> {
                   new SqlParameter { ParameterName = "@IDdash", Value = das },
                new SqlParameter { ParameterName = "@mess", Value = mess },
                new SqlParameter { ParameterName = "@visitor", Value = visitor },
                new SqlParameter { ParameterName = "@email", Value = email },
                new SqlParameter { ParameterName = "@phone", Value = phone }
            };
            var x = _StoreProduce.Run<ChatLine>(new StoreProduceModel { NameProduce = "FitterMutitle", Params = Params });
            return x;
        }
        public IEnumerable<DateTime> getlasttimeonline(string idagent)
        {
            IEnumerable<SqlParameter> Params = new List<SqlParameter> {
                 new SqlParameter { ParameterName = "@idagent", Value = idagent } };

            return _StoreProduce.Run<DateTime>(new StoreProduceModel { NameProduce = "getlasttimeonline", Params = Params });
        }
    }
}
