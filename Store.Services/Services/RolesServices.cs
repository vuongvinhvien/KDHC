using Store.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Sevices.Services
{
    public interface IRolesServices
    {

        bool InRole(string Email, string nameroles);
    }
    public class RolesServices: IRolesServices
    {
        private readonly IAccountResponsitory _User;
        private readonly IRolesResponsitory _Roles;
        private readonly IAspNetRoleResponsitory _RolesNet;
        private readonly IStoreProduce _StoreProduce;
        public RolesServices(IAccountResponsitory User, IStoreProduce StoreProduce, IRolesResponsitory Roles, IAspNetRoleResponsitory RolesNet)
        {
            _User = User;          
            _StoreProduce = StoreProduce;
            _Roles = Roles;
            _RolesNet = RolesNet;
        }

        public bool InRole(string Email,string nameroles)
        {
            var AccountID = _User.GetAll().Where(x => x.Email.Equals(Email)).FirstOrDefault().Id;
            var rolesid = _Roles.GetAll().Where(x => x.UserId.Equals(AccountID)).FirstOrDefault().RoleId;
            return _RolesNet.GetById(rolesid).Name.Equals(nameroles);
        }
    }
}