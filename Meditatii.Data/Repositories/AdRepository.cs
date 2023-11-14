using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Enums;
using Meditatii.Core.Helpers;
using Meditatii.CoreNew.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Repositories
{
    public class AdRepository : IAdData
    {
        public List<Ad> GetAdsForUser(int userId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var ads = context.Set<Models.Ad>().Where(x => x.TeacherId == userId).AsNoTracking().AsQueryable();
                    return MappingHelper.Map<List<Ad>>(ads.ToList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public void AdView(int adId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("insert into AdViews (AdId) Values (" + adId + ")");

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public int GetNrOfViewsForAd(int adId)
        {
            int result = 0;
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    result = context.Database.SqlQuery<int>("select count(*) from [emeditatii].[dbo].[AdViews] where adid=" + adId).Single();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
            
        }

        public Ad GetAd(int id)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var ads = context.Set<Models.Ad>()
                        .AsNoTracking().AsQueryable();
                    return MappingHelper.Map<Ad>(ads.Where(x => x.Id == id).FirstOrDefault());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public SearchResult<Ad> GetAds(int? categoryId, int? cycleId, int? cityId, int? order, int skip, int take)
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

                        var users = context.Set<Models.Ad>().Where(x => !String.IsNullOrEmpty(x.Teacher.FirstName) &&
                                                                          !String.IsNullOrEmpty(x.Teacher.LastName) &&
                                                                          x.Teacher.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null &&
                                                                          x.Teacher.Active &&
                                                                          x.Price != null
                                                                          ).AsNoTracking().AsQueryable();

                        if (categoryId != null && categoryId > 0)
                        {
                            users = users.Where(x => x.Category.Id == categoryId);
                        }

                        if (cityId != null && cityId > 0)
                        {
                            users = users.Where(x => x.Teacher.Cities.Count(z => z.Id == cityId) > 0);
                        }

                        totalRows = users.Count();

                        //['Cele mai populare','Nr. review-uri','Cele mai noi']
                        if (order == null)
                        {
                            return new SearchResult<Ad>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<Ad>>(users.OrderBy(x => x.Teacher.SubscriptionStartDate).ThenBy(x => x.Id).Skip(skip).Take(take).ToList())
                            };
                        }
                        else if (order.Value == 1)
                        {
                            return new SearchResult<Ad>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<Ad>>(users.OrderByDescending(x => x.Teacher.SubscriptionStartDate).Skip(skip).Take(take).ToList())
                            };
                        }
                        else if (order.Value == 2)
                        {
                            return new SearchResult<Ad>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<Ad>>(users.OrderByDescending(x => x.Teacher.Rating).Skip(skip).Take(take).ToList())
                            };
                        }
                        else
                        {
                            return new SearchResult<Ad>
                            {
                                TotalRows = totalRows,
                                Entities = MappingHelper.Map<List<Ad>>(users.OrderByDescending(x => x.Teacher.Added).Skip(skip).Take(take).ToList())
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

        public SearchResult<Ad> GetAll(int skip, int take, int? order)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var users = context.Set<Models.Ad>().Where(x => !String.IsNullOrEmpty(x.Teacher.FirstName) &&
                                                                      !String.IsNullOrEmpty(x.Teacher.LastName) &&
                                                                      x.Teacher.Roles.Where(y => y.Name == UserType.Teacher.ToString()).FirstOrDefault() != null &&
                                                                      x.Teacher.Active &&
                                                                      x.Price != null).AsNoTracking();

                    //grouping
                    users = users.GroupBy(x => x.Teacher).Select(x => x.FirstOrDefault());

                    totalRows = users.Count();
                    if (order == null)
                    {
                        return
                            new SearchResult<Ad>
                            {
                                Entities = MappingHelper.Map<List<Ad>>(
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
                            new SearchResult<Ad>
                            {
                                Entities = MappingHelper.Map<List<Ad>>(
                                                                        users.OrderByDescending(x => x.Added)
                                                                            .Skip(skip)
                                                                            .Take(take)
                                                                            .ToList()),
                                TotalRows = totalRows
                            };
                        /*new SearchResult<Ad>
                        {
                            Entities = MappingHelper.Map<List<Ad>>(
                                                                    users.OrderByDescending(x => x.Teacher.NrOfVisitors)
                                                                        .Skip(skip)
                                                                        .Take(take)
                                                                        .ToList()),
                            TotalRows = totalRows
                        };*/
                    }
                    else if (order.Value == 2)
                    {
                        return
                            new SearchResult<Ad>
                            {
                                Entities = MappingHelper.Map<List<Ad>>(
                                                                        users.OrderByDescending(x => x.Teacher.Rating)
                                                                            .Skip(skip)
                                                                            .Take(take)
                                                                            .ToList()),
                                TotalRows = totalRows
                            };
                    }
                    else
                    {
                        return
                            new SearchResult<Ad>
                            {
                                Entities = MappingHelper.Map<List<Ad>>(
                                                                        users.OrderByDescending(x => x.Teacher.Added)
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

        public void SaveAdForUser(Ad ad)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    Models.Ad entity = MappingHelper.Map<Models.Ad>(ad);

                    context.Entry(entity).State = entity.Id > 0 ? EntityState.Modified : EntityState.Added;

                    foreach (var cycleItem in entity.Cycles)
                    {
                        context.Entry(cycleItem).State = EntityState.Unchanged;
                    }
                    context.Entry(entity.Category).State = EntityState.Unchanged;
                    context.SaveChanges();

                    //update cycles 
                    if (ad.Id > 0)
                    {
                        //remove old selected
                        context.Database.ExecuteSqlCommand("delete from AdCycle where AdId = " + ad.Id);

                        foreach (Cycle cycle in ad.Cycles)
                        {
                            context.Database.ExecuteSqlCommand("insert into AdCycle (AdId, CycleId) Values (" + entity.Id + ", " + cycle.Id + ")");
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public void DeleteAdForUser(Ad ad)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("delete from AdCycle where AdId = " + ad.Id);
                    context.Database.ExecuteSqlCommand("delete from Ad where Id = " + ad.Id);
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
