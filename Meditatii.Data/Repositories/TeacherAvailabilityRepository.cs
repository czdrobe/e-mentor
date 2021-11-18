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
    public class TeacherAvailabilityRepository : ITeacherAvailabilityData
    {
        public SearchResult<TeacherAvailability> GetAvailability(string useremail, int skip, int take)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;

                    var availabilities = context.Set<Models.TeacherAvailability>().AsNoTracking().Where(x => x.Teacher.Email == useremail);

                    totalRows = availabilities.Count();

                    return
                        new SearchResult<TeacherAvailability> {
                            Entities = MappingHelper.Map<List<TeacherAvailability>>(
                                                                    availabilities.OrderBy(x => x.Id)
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

        public SearchResult<TeacherAvailability> GetTeacherAvailabilityForDay(int userId, int day, bool isToday)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    int totalRows = 0;
                    IQueryable<Models.TeacherAvailability> availabilities;
                    if (isToday)
                    {
                        availabilities = context.Set<Models.TeacherAvailability>().AsNoTracking().Where(x => x.Teacher.Id == userId && x.Day == day && x.Time > DateTime.Now.Hour);
                    }
                    else
                    {
                        availabilities = context.Set<Models.TeacherAvailability>().AsNoTracking().Where(x => x.Teacher.Id == userId && x.Day == day);
                    }

                    totalRows = availabilities.Count();

                    return
                        new SearchResult<TeacherAvailability>
                        {
                            Entities = MappingHelper.Map<List<TeacherAvailability>>(
                                                                    availabilities.OrderBy(x => x.Id)
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

        public void RemoveAllAvailabilityForTeacher(string useremail)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    context.Database.ExecuteSqlCommand("delete from TeacherAvailability where TeacherId in (select id from [user] where UserName = '" + useremail + "')");
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveNewAvailability(string useremail, int day, int Time)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsNoTracking().AsQueryable();
                    var user = MappingHelper.Map<User>(users.Where(x => x.Email == useremail).FirstOrDefault());

                    context.TeacherAvailabilities.Add(new Models.TeacherAvailability()
                    {
                        Day = day,
                        TeacherId = user.Id,
                        Time = Time
                    });
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
