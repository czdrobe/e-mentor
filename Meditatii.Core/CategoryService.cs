using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core
{
    public interface ICategoryService:ILazyLoadable
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Category> GetMains();

        IEnumerable<Category> GetSubcategories(int id);

        IEnumerable<Category> GetAllWithSubcategories();

        void SaveCategoriesForUser(string username, List<Category> lstCategories);
    }
}
