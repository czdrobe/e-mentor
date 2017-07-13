using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
