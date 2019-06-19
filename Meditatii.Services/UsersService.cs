using Meditatii.Core;
using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Services
{
    public class UsersService:  IUsersService
    {
        private IUserData userData;
        public UsersService(IUserData userData)
        {
            this.userData = userData;
        }
        
        public SearchResult<User> GetUsers(int? categoryId, int? cycleId, int skip, int take)
        {
            return userData.GetUsers(categoryId, cycleId, skip, take);
        }

        public User GetUser(int userId)
        {
            return userData.GetUser(userId);
        }

        public User GetUser(string useremail)
        {
            return userData.GetUser(useremail);
        }

        public void SaveUser(User user)
        {
            userData.SaveUser(user);
        }
    }
}
