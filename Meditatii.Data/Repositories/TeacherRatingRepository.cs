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
                    //we only have the appoitmentid and the rating
                    //need to find the appoitment and get the teacher id and studentid
                    //also check if the current logged user is the student because just this can rate the teacher

                    var appoitment = context.Set<Models.Appoitment>().Where(x => x.Id == rating.AppoitmentId)
                                .AsNoTracking()
                                .FirstOrDefault();

                    if (appoitment != null && appoitment.Learner.Email == currentUserEmail)
                    {
                        context.Database.ExecuteSqlCommand("insert into TeacherRating (AppoitmentId, TeacherId, StudentId, Rating, Added) Values (" + rating.AppoitmentId + ", " + appoitment.TeacherId + "," + appoitment.LearnerId + "," + rating.Rating + "," + "'" + DateTime.Now + "')");
                        context.SaveChanges();

                        //recalculate rating
                        CalculateRatingForTeacher(appoitment.TeacherId);
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
                    decimal newrating = (decimal)context.Set<Models.TeacherRating>().Where(x => x.TeacherId == teacherId).Average(y => y.Rating);

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
