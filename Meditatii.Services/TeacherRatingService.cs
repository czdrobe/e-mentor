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
    public class TeacherRatingService : ITeacherRatingService
    {
        private ITeacherRatingData teacherRatingData;
        public TeacherRatingService(ITeacherRatingData teacherRatingData)
        {
            this.teacherRatingData = teacherRatingData;
        }

        public IEnumerable<TeacherRating> GetAll()
        {
            return teacherRatingData.GetAll();
        }

        public IEnumerable<TeacherRating> GetAllRatingForTeacher(int teacherId)
        {
            return teacherRatingData.GetAllRatingForTeacher(teacherId);
        }

        public void SaveRatingForTeacher(TeacherRating rating, string currentUserEmail)
        {
            this.teacherRatingData.SaveRatingForTeacher(rating, currentUserEmail);
        }

        public void CalculateRatingForTeacher(int teacherId)
        {
            this.teacherRatingData.CalculateRatingForTeacher(teacherId);
        }
    }
}
