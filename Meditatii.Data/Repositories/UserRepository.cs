using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Enums;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Repositories
{
    public class UserRepository : IUserData
    {
        public SearchResult<User> GetAll(int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var users = context.Set<Models.User>().Where(x => x.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null)
                        .AsNoTracking();

                    totalRows = users.Count();

                    return
                        new SearchResult<User> {
                            Entities = MappingHelper.Map<List<User>>(
                                                                    users.OrderBy(x => x.Id)
                                                                        .Skip(skip)
                                                                        .Take(take)
                                                                        .ToList()),
                            TotalRows = totalRows
                        };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public SearchResult<User> GetUsers(int? categoryId, int? cycleId, int skip, int take)
        {
            if ((categoryId != null && categoryId > 0) ||
                (cycleId != null && cycleId > 0))
            {
                using (var context = new MeditatiiDbContext())
                {
                    try
                    {
                        int totalRows = 0;

                        var users = context.Set<Models.User>().Where(x => !String.IsNullOrEmpty(x.FirstName) && !String.IsNullOrEmpty(x.LastName) && x.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null)
                            .AsNoTracking().AsQueryable();

                        if (categoryId != null && categoryId > 0)
                        {
                            users = users.Where(x => x.Categories.Count(y => y.Id == categoryId) > 0);
                        }


                        if (cycleId != null && cycleId > 0)
                        {
                            users = users.Where(x => x.Cycles.Count(z => z.Id == cycleId) > 0);
                        }

                        totalRows = users.Count();

                        return new SearchResult<User>
                        {
                            TotalRows = totalRows,
                            Entities = MappingHelper.Map<List<User>>(users.OrderBy(x => x.Id).Skip(skip).Take(take).ToList())
                        };
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                return GetAll(skip, take);
            }
        }

        public User GetUser(int userId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsNoTracking().AsQueryable();
                    return MappingHelper.Map<User>(users.Where(x => x.Id == userId).FirstOrDefault());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public User GetUser(string useremail)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsNoTracking().AsQueryable();
                    var user = MappingHelper.Map<User>(users.Where(x => x.Email == useremail).FirstOrDefault());

                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveUser(User user)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Entry(MappingHelper.Map<Models.User>(user)).State = user.Id > 0 ? EntityState.Modified : EntityState.Added;
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
