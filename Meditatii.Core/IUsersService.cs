using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface IUsersService:ILazyLoadable
    {
        SearchResult<User> GetUsers(int? categoryId, int? cycleId, int skip, int take);
        User GetUser(int userId);
    }
}
