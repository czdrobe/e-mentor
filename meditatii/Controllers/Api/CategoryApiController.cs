using meditatii.Models;
using Meditatii.Core;
using Meditatii.Core.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace meditatii.Controllers.Api
{
    public class CategoryApiController : ApiController
    {
        private ICategoryService categoryService;

        public CategoryApiController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Route("api/categories/getall")]
        public List<Models.CategoryModel> GetAll()
        {
            return MappingHelper.Map<List<CategoryModel>>(this.categoryService.GetCategories());
        }

        [HttpGet]
        [Route("api/categories/getmains")]
        public List<Models.CategoryModel> GetMains()
        {
            return MappingHelper.Map<List<CategoryModel>>(this.categoryService.GetMains());
        }

        [HttpGet]
        [Route("api/categories/getallwithsubcategories")]
        public List<Models.CategoryModel> getallwithsubcategories()
        {
            return MappingHelper.Map<List<CategoryModel>>(this.categoryService.GetAllWithSubcategories());
        }

        [HttpGet]
        [Route("api/categories/getsubcategories")]
        public List<Models.CategoryModel> GetSubcategories(int id)
        {
            return MappingHelper.Map<List<CategoryModel>>(this.categoryService.GetSubcategories(id));
        }
    }
}