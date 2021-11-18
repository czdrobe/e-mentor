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

        public IEnumerable<Category> GetAllWithSubcategories()
        {
            var mainCategories = categoryData.GetMains();
            List<Category> allCategoriesWithParentName = new List<Category>();

            foreach (var mainCategory in mainCategories)
            {
                var subCategories = categoryData.GetSubcategories(mainCategory.Id);
                foreach (var category in subCategories)
                {
                    allCategoriesWithParentName.Add(new Category()
                    {
                        //Name = mainCategory.Name + " - " + category.Name,
                        Name = category.Name,
                        Id = category.Id
                    });
                }
            }

            return allCategoriesWithParentName;
        }

        public void SaveCategoriesForUser(string useremail, List<Category> lstCategories)
        {
            categoryData.RemoveAllCategoriesForUser(useremail);
            foreach (var category in lstCategories)
            {
                categoryData.SaveCategoryForUser(useremail, category);
            }
        }
    }
}
