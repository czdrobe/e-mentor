using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Meditatii.Data.Repositories
{
    public class CategoryRepository : ICategoryData
    {
        public IEnumerable<Category> GetAll()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.Category>()
                        .AsNoTracking()
                        .ToList();
                    return MappingHelper.Map<List<Category>>(users);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public IEnumerable<Category> GetMains()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.Category>()
                        .AsNoTracking()
                        .Where(x => x.ParentId == 0)
                        .ToList();
                    return MappingHelper.Map<List<Category>>(users);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public IEnumerable<Category> GetSubcategories(int id)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.Category>()
                        .AsNoTracking()
                        .Where(x => x.ParentId == id)
                        .ToList();
                    return MappingHelper.Map<List<Category>>(users);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void RemoveAllCategoriesForUser(string useremail)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("delete from UserCategory where UserId in (select id from [user] where UserName = '" + useremail + "')");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveCategoryForUser(string useremail, Category category)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsQueryable();
                    var user = MappingHelper.Map<User>(users.Where(x => x.Email == useremail).FirstOrDefault());

                    context.Database.ExecuteSqlCommand("insert into UserCategory (UserId, CategoryId) Values (" +user.Id + ", " + category.Id + ")");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
