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



namespace Store.Sevices.Services
{
    public interface IAccountSevices
    {
        bool VerifyAccount(string userName, string password);
    }
    public class AccountSevices : IAccountSevices
      {
        private readonly IAccountResponsitory _User;
        private readonly IPasswordHasher _passwordHasher;

        public AccountSevices(IAccountResponsitory User, IPasswordHasher passwordHasher)
        {
            _User = User;
            _passwordHasher = passwordHasher;

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
    }
}
