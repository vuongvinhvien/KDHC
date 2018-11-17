using Microsoft.AspNet.Identity;
using Store.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.DataDbContext;

namespace Store.Sevices.Services
{
    public interface ISettingSevice
    {

        Setting GetSettingByDashBoard(string id);
        void SetDefaultSetting(string Idcustomer, string Email);
        Setting EditSetting(string ID_dash, string emailsend, bool sound, bool sharefileAgent, bool sharefileVisitor);
    }
    public class SettingSevice : ISettingSevice
    {
        private readonly IAccountResponsitory _User;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IStoreProduce _StoreProduce;
        private readonly ICustomerResponsitory _DashBoard;
        private readonly ISettingResponsitory  _Setting;
        public SettingSevice(IAccountResponsitory User, IPasswordHasher passwordHasher, IStoreProduce StoreProduce, ICustomerResponsitory DashBoard, ISettingResponsitory Setting)
        {
            _User = User;
            _passwordHasher = passwordHasher;
            _StoreProduce = StoreProduce;
            _DashBoard = DashBoard;
            _Setting = Setting;
        }
        public Setting GetSettingByDashBoard(string id)
        {
            return _Setting.GetAll().Where(x=>x.ID_Customer == id).FirstOrDefault();
        }
        public void SetDefaultSetting(string Idcustomer,string Email)
        {
            _Setting.Add(new Setting { ID_Customer = Idcustomer, EmailSendScript = Email, ShareFileAgent = true, ID_Setting = Guid.NewGuid().ToString(), ShareFileVisitor = true, Sound = true });
        }
     
        public Setting EditSetting(string ID_dash, string emailsend, bool sound, bool sharefileAgent, bool sharefileVisitor)
        {

            var Setting = GetSettingByDashBoard(ID_dash);
            Setting.EmailSendScript = emailsend;
            Setting.Sound = sound;
            Setting.ShareFileAgent = sharefileAgent;
            Setting.ShareFileVisitor = sharefileVisitor;
            _Setting.Update(Setting);
            return Setting;
        }
    }
}
