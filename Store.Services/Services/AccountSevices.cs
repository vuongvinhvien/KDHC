using Microsoft.AspNet.Identity;
using Store.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Store.Data.DataDbContext;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;

namespace Store.Sevices.Services
{
    public interface IAccountSevices
    {
        bool VerifyAccount(string userName, string password);
        string GetRoles(string ID);
        bool isMain(string id);
        IEnumerable<AspNetUser> GetByCustomer(string id);
        IEnumerable<AspNetUser> GetAllMainAccount();
        AspNetUser GetByID(string id);
        void DeleteAccount(string id);
        void UpdateAccount(AspNetUser User);
    }
    public class AccountSevices : IAccountSevices
      {
        private readonly IAccountResponsitory _User;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IStoreProduce _StoreProduce;
        private readonly ICustomerResponsitory _Customer;


        public AccountSevices(ICustomerResponsitory Customer,IAccountResponsitory User, IPasswordHasher passwordHasher, IStoreProduce StoreProduce)
        {
            _User = User;
            _passwordHasher = passwordHasher;
            _StoreProduce = StoreProduce;
            _Customer = Customer;
            }
         public bool VerifyAccount(string userName, string password)
        {
            var oldAccount = _User.GetAll();
            var Result = oldAccount.Where(c => c.UserName.ToLower() == userName.ToLower() || c.Email.ToLower() == userName.ToLower()).FirstOrDefault();
            if (Result != null)
            {
                return _passwordHasher.VerifyHashedPassword(Result.PasswordHash, password) == PasswordVerificationResult.Success;
            }

            return false;
        }
        public string GetRoles(string ID)
        {
          return _StoreProduce.Run<string>(new StoreProduceModel { NameProduce = "GetRolesByID", Params = new List<SqlParameter> { new SqlParameter { ParameterName = "@ID", Value = ID } }}).FirstOrDefault();
        }
      
    
        public bool isMain(string id)
        {
            return _User.GetById(id).IsMain == true;
        }
        public IEnumerable<AspNetUser> GetByCustomer(string id)
        {
            return _User.GetAll().Where(x => x.ID_Customer == id);
        }
        public IEnumerable<AspNetUser> GetAgent(string id)
        {
            return _User.GetAll().Where(x => x.ID_Customer == id);
        }

        public IEnumerable<AspNetUser> GetAllMainAccount()
        {
            return _User.GetAll().Where(x => x.IsMain == true);
        }

        public AspNetUser GetByID(string id)
        {
            return _User.GetById(id);
        }
        public void DeleteAccount(string id)
        {
            var current = _User.GetById(id);
            current.status = false;
            _User.Update(current);
                
        }
        public void UpdateAccount(AspNetUser User)
        {
            
            _User.Update(User);

        }
        

    }
}
