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
        public SearchResult<User> GetAll(int skip, int take, int? order)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var users = context.Set<Models.User>().Where(x => !String.IsNullOrEmpty(x.FirstName) &&
                                                                      !String.IsNullOrEmpty(x.LastName) &&
                                                                      x.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null &&
                                                                      x.Active &&
                                                                      x.Price != null &&
                                                                      x.Price > 0).AsNoTracking();

                    totalRows = users.Count();
                    if (order == null)
                    {
                        return
                            new SearchResult<User>
                            {
                                Entities = MappingHelper.Map<List<User>>(
                                                                        users.OrderBy(x => x.Id)
                                                                            .Skip(skip)
                                                                            .Take(take)
                                                                            .ToList()),
                                TotalRows = totalRows
                            };
                    }
                    else if (order.Value == 1)
                    {
                        return
                            new SearchResult<User>
                            {
                                Entities = MappingHelper.Map<List<User>>(
                                                                        users.OrderByDescending(x => x.NrOfVisitors)
                                                                            .Skip(skip)
                                                                            .Take(take)
                                                                            .ToList()),
                                TotalRows = totalRows
                            };
                    }
                    else if (order.Value == 2)
                    {
                        return
                            new SearchResult<User>
                            {
                                Entities = MappingHelper.Map<List<User>>(
                                                                        users.OrderByDescending(x => x.Rating)
                                                                            .Skip(skip)
                                                                            .Take(take)
                                                                            .ToList()),
                                TotalRows = totalRows
                            };
                    }
                    else
                    {
                        return
                            new SearchResult<User>
                            {
                                Entities = MappingHelper.Map<List<User>>(
                                                                        users.OrderByDescending(x => x.Added)
                                                                            .Skip(skip)
                                                                            .Take(take)
                                                                            .ToList()),
                                TotalRows = totalRows
                            };
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public SearchResult<User> GetTeachersForNewsletter(int lastTeacherId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var users = context.Set<Models.User>().Where(x => x.ProfileImageUrl != "/profileimages/nouser.png" &&
                                                                      x.Id > lastTeacherId &&
                                                                      x.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null &&
                                                                      x.Active
                                                                      ).AsNoTracking().AsQueryable();
                    //filter out current user
                    totalRows = users.Count();

                    return new SearchResult<User>
                    {
                        TotalRows = totalRows,
                        Entities = MappingHelper.Map<List<User>>(users.OrderBy(x => x.Added).Take(6).ToList())
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public SearchResult<User> GetStudentsForNewsletter(int? studentId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var users = context.Set<Models.User>().Where(x => x.Roles.Where(y => y.Name == UserType.Student.ToString()).FirstOrDefault() != null 
                                                                        && x.Active
                                                                        && x.SendNewsletter
                                                                      ).AsNoTracking().AsQueryable();

                    if (studentId != null && studentId > 0)
                    {
                        users = users.Where(x => x.Id == studentId);
                    }
                    //filter out current user
                    totalRows = users.Count();

                    return new SearchResult<User>
                    {
                        TotalRows = totalRows,
                        Entities = MappingHelper.Map<List<User>>(users.OrderBy(x => x.Added).ToList())
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public SearchResult<Request> GetRequests(string city, string subject, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var requests = context.Set<Models.Request>().Where(
                        x => x.StartDate <= DateTime.Now &&
                        x.EndDate >= DateTime.Now &&
                        x.Active).AsNoTracking().AsQueryable();

                    if (!String.IsNullOrEmpty(city))
                    {
                        requests = requests.Where(x => x.City.Name.Contains(city));
                    }

                    if (!String.IsNullOrEmpty(subject))
                    {
                        requests = requests.Where(x => x.Description.Contains(subject));
                    }

                    totalRows = requests.Count();

                    return new SearchResult<Request>
                    {
                        Entities = MappingHelper.Map<List<Request>>(
                                                                        requests.OrderByDescending(x => x.StartDate)
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

        public SearchResult<User> GetSuggestedUsers(int userId, int? categoryId, int? cityId)
        {
            if ((categoryId != null && categoryId > 0) ||
                (cityId != null && cityId > 0))
            {
                using (var context = new MeditatiiDbContext())
                {
                    try
                    {
                        int totalRows = 0;

                        var users = context.Set<Models.User>().Where(x => !String.IsNullOrEmpty(x.FirstName) &&
                                                                          !String.IsNullOrEmpty(x.LastName) &&
                                                                          x.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null &&
                                                                          x.Active &&
                                                                          x.Price != null &&
                                                                          x.Price > 0
                                                                          ).AsNoTracking().AsQueryable();
                        //filter out current user
                        if (userId > 0)
                        {
                            users = users.Where(x => x.Id != userId);
                        }

                        IQueryable<Models.User> usersWithinCategory = null;
                        /*if (categoryId != null && categoryId > 0)
                        {
                            usersWithinCategory = users.Where(x => x.Categories.Count(y => y.Id == categoryId) > 0);
                            users = usersWithinCategory;
                        }*/


                        if (cityId != null && cityId > 0)
                        {
                            users = users.Where(x => x.Cities.Count(z => z.Id == cityId) > 0);
                        }


                        if (users.Count() == 0)
                        {
                            users = usersWithinCategory;
                        }
                        totalRows = users.Count();

                        return new SearchResult<User>
                        {
                            TotalRows = totalRows,
                            Entities = MappingHelper.Map<List<User>>(users.OrderByDescending(x => x.Added).Take(10).ToList())
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
                return new SearchResult<User>();
            }

        }

        public SearchResult<User> GetUsers(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take)
        {
            if ((categoryId != null && categoryId > 0) ||
                (cycleId != null && cycleId > 0) ||
                (cityId != null && cityId > 0))
            {
                using (var context = new MeditatiiDbContext())
                {
                    try
                    {
                        int totalRows = 0;

                        var users = context.Set<Models.User>().Where(x => !String.IsNullOrEmpty(x.FirstName) &&
                                                                          !String.IsNullOrEmpty(x.LastName) &&
                                                                          x.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null &&
                                                                          x.Active &&
                                                                          x.Price != null &&
                                                                          x.Price > 0
                                                                          ).AsNoTracking().AsQueryable();

                        /*if (categoryId != null && categoryId > 0)
                        {
                            users = users.Where(x => x.Categories.Count(y => y.Id == categoryId) > 0);
                        }*/

                        if (cityId != null && cityId > 0)
                        {
                            users = users.Where(x => x.Cities.Count(z => z.Id == cityId) > 0);
                        }

                        totalRows = users.Count();

                        //['Cele mai populare','Nr. review-uri','Cele mai noi']
                        if (order == null)
                        {
                            return new SearchResult<User>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<User>>(users.OrderBy(x => x.SubscriptionStartDate).ThenBy(x=>x.Id).Skip(skip).Take(take).ToList())
                            };
                        }
                        else if (order.Value == 1)
                        {
                            return new SearchResult<User>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<User>>(users.OrderByDescending(x => x.SubscriptionStartDate).Skip(skip).Take(take).ToList())
                            };
                        }
                        else if (order.Value == 2)
                        {
                            return new SearchResult<User>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<User>>(users.OrderByDescending(x => x.Rating).Skip(skip).Take(take).ToList())
                            };
                        }
                        else
                        {
                            return new SearchResult<User>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<User>>(users.OrderByDescending(x => x.Added).Skip(skip).Take(take).ToList())
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                return GetAll(skip, take, order);
            }
        }

        public UserSats GetUserStats()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {

                    UserSats stats = new UserSats();

                    var allusers = context.Set<Models.User>()
                        .AsNoTracking().AsQueryable();

                    stats.TotalUser = allusers.Count();
                    stats.TotalTeacher = allusers.Where(x => x.Roles.Where(y => y.Id == 1).Count() > 0).Count();
                    stats.TotalMeetingMinutes = 592; //to do - to not hard code - get real value

                    return stats;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

        public Request GetRequest(int requestId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var requests = context.Set<Models.Request>()
                        .AsNoTracking().AsQueryable();
                    return MappingHelper.Map<Request>(requests.Where(x => x.Id == requestId).FirstOrDefault());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public int SaveNewRequest(Request newRequest)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    Models.Request entity = MappingHelper.Map<Models.Request>(newRequest);

                    context.Entry(entity).State = entity.Id > 0 ? EntityState.Modified : EntityState.Added;

                    context.SaveChanges();

                    return entity.Id;
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

        public void ActivateUser(int userId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("update [user] set Active = 1 where Id = " + userId);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void IncrementVisitorsNumber(int userId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("update [user] set NrOfVisitors = NrOfVisitors + 1 where Id = " + userId);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void IncrementPhoneViews(int userId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("update [user] set NrOfPhoneViews = NrOfPhoneViews + 1 where Id = " + userId);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void LogLogin(int userId, int success)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("insert into LogLogin (UserId, Success) values (" + userId + ", " + success + ") ");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public User UpdateSubscriptionPeriod(int userId, int period)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(period);
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsNoTracking().AsQueryable();
                    var user = MappingHelper.Map<User>(users.Where(x => x.Id == userId).FirstOrDefault());

                    context.Database.ExecuteSqlCommand("update [user] set SubscriptionStartDate = '" + startDate.ToString("yyyy-MM-dd") + "', SubscriptionEndDate='" + endDate.ToString("yyyy-MM-dd")+ "'  where Id = " + userId);
                    user.SubscriptionStartDate = startDate;
                    user.SubscriptionEndDate = endDate;

                    context.SaveChanges();

                    return MappingHelper.Map<User>(user);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<City> GetCities()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    return MappingHelper.Map<List<City>>(context.Set<Models.City>().Where(x => x.Population > 20000).OrderBy(x => x.FullName).AsNoTracking().AsQueryable());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public List<Experience> GetExperiences()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    return MappingHelper.Map<List<Experience>>(context.Set<Models.Experience>().AsNoTracking().AsQueryable());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<Occupation> GetOccupations()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    return MappingHelper.Map<List<Occupation>>(context.Set<Models.Occupation>().AsNoTracking().AsQueryable());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveCityForUser(string username, int city1, bool isOnline, bool atTeacher, bool atStudent)
        {
            if (city1 <= 0 && !isOnline)
                return; //do nothing if there is no cityid

            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsNoTracking().AsQueryable();
                    var user = MappingHelper.Map<User>(users.Where(x => x.Email == username).FirstOrDefault());

                    if (user != null)
                    {
                        //delete all existing city associated to the user
                        context.Database.ExecuteSqlCommand("delete from CityUser where userid=" + user.Id);
                        context.SaveChanges();

                        if (city1 > 0)
                        {
                            context.Database.ExecuteSqlCommand("INSERT INTO [dbo].[CityUser] ([CityId],[UserId]) VALUES (" + city1+ "," + user.Id + ")");
                            context.SaveChanges();
                        }
                        context.Database.ExecuteSqlCommand("update [User] set [AlsoOnline] = " + (isOnline ? "1" : "0") 
                            + ", [AtTeacher] = " + (atTeacher ? "1" : "0") + ", [AtStudent] = " + (atStudent ? "1" : "0") + " where id = " + user.Id);
                        context.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }


    }
}
