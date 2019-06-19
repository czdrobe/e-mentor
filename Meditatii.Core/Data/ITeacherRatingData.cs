using Meditatii.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Data
{
    public interface ITeacherRatingData : ILazyLoadable
    {
        IEnumerable<TeacherRating> GetAll();

        IEnumerable<TeacherRating> GetAllRatingForTeacher(int teacherId);

        void SaveRatingForTeacher(TeacherRating rating, string currentUserEmail);

        void CalculateRatingForTeacher(int teacherId);
    }
}
