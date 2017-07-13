using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface IUserData: ILazyLoadable
    {
        SearchResult<User> GetAll(int skip, int take);

        SearchResult<User> GetUsers(int? categoryId, int? cycleId, int skip, int take);
    }
}
