using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class TeacherRatingApiController : ApiController
    {
        private ITeacherRatingService teacherRatingService;

        public TeacherRatingApiController(ITeacherRatingService teacherRatingService)
        {
            this.teacherRatingService = teacherRatingService;
        }

        [HttpGet]
        [Route("api/teacherrating/getall")]
        public List<Models.TeacherRatingModel> GetAll()
        {
            return MappingHelper.Map<List<TeacherRatingModel>>(this.teacherRatingService.GetAll());
        }

        [HttpGet]
        [Route("api/teacherrating/getallratingforteacher")]
        public List<Models.TeacherRatingModel> GetAllRatingForTeacher(int teacherid)
        {
            return MappingHelper.Map<List<TeacherRatingModel>>(this.teacherRatingService.GetAllRatingForTeacher(teacherid));
        }

        [HttpPost]
        [Route("api/teacherrating/getallratingforteacher")]
        public void SaveRatingForTeacher(TeacherRatingModel teacherrating)
        {
            this.teacherRatingService.SaveRatingForTeacher(MappingHelper.Map<Meditatii.Core.Entities.TeacherRating>(teacherrating), System.Web.HttpContext.Current.User.Identity.Name);
        }

    }
}