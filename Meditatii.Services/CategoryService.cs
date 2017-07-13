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
    public class CategoryService:  ICategoryService
    {
        private ICategoryData categoryData;
        public CategoryService(ICategoryData categoryData)
        {
            this.categoryData = categoryData;
        }
        
        public IEnumerable<Category> GetCategories()
        {
            return categoryData.GetAll();
        }

        public IEnumerable<Category> GetMains()
        {
            return categoryData.GetMains();
        }

        public IEnumerable<Category> GetSubcategories(int id)
        {
            return categoryData.GetSubcategories(id);
        }

    }
}
