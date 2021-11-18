using Meditatii.Core.Data;
using Meditatii.Core.Entities;
using Meditatii.Core.Enums;
using Meditatii.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Repositories
{
    public class TeacherRatingRepository : ITeacherRatingData
    {

        public IEnumerable<TeacherRating> GetAll()
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var ratings = context.Set<Models.TeacherRating>()
                        .AsNoTracking()
                        .ToList();
                    return MappingHelper.Map<List<TeacherRating>>(ratings);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<TeacherRating> GetAllRatingForTeacher(int teacherId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var ratings = context.Set<Models.TeacherRating>().Where(x => x.TeacherId == teacherId).AsNoTracking().AsQueryable();

                    return MappingHelper.Map<List<TeacherRating>>(ratings.ToList());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void SaveRatingForTeacher(TeacherRating rating, string currentUserEmail)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    var users = context.Set<Models.User>()
                        .AsQueryable();

                    var user = MappingHelper.Map<User>(users.Where(x => x.Email  == currentUserEmail).FirstOrDefault());

                    if (user != null)
                    {
                        string sql = "insert into TeacherRating (AppoitmentId, TeacherId, StudentId, Rating, Added, RatingText) Values (null, @teacherId, @studentId, @rating, @added, @ratingtext)";
                        context.Database.ExecuteSqlCommand(sql, 
                                                                new SqlParameter("@teacherId", rating.TeacherId),
                                                                new SqlParameter("@studentId", user.Id),
                                                                new SqlParameter("@rating", rating.Rating),
                                                                new SqlParameter("@added", DateTime.Now),
                                                                new SqlParameter("@ratingtext", rating.RatingText));
                        context.SaveChanges();

                        //recalculate rating
                        CalculateRatingForTeacher(rating.TeacherId);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void CalculateRatingForTeacher(int teacherId)
        {
            using (var context = new MeditatiiDbContext())
            {
                try
                {
                    decimal newrating = (decimal)context.Set<Models.TeacherRating>()
                                                                .Where(x => x.TeacherId == teacherId)
                                                                .GroupBy(x => x.TeacherId, r => r.Rating)
                                                                .Select(x => x.Average()).FirstOrDefault();

                    var users = context.Set<Models.User>()
                        .AsQueryable();

                    var user = MappingHelper.Map<User>(users.Where(x => x.Id == teacherId).FirstOrDefault());

                    if (user != null)
                    {
                        user.Rating = Convert.ToInt32(newrating * 100);
                        context.SaveChanges(); //save new rating
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
