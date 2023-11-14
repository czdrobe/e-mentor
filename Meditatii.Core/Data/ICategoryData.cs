using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface ICategoryData: ILazyLoadable
    {
        IEnumerable<Category> GetAll();

        IEnumerable<CategoryGroup> GetAllGroupped();

        IEnumerable<Category> GetMains();

        Category GetCategoryByName(string name);

        IEnumerable<Category> GetSubcategories(int id);

        void SaveCategoryForUser(string username, Category category);

        void RemoveAllCategoriesForUser(string useremail);
    }
}
