using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService = new CategoryService();
        
        //GET http://localhost:5000/api/categories
        [Route("")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_categoryService.GetCategories());
        }

        //GET http://localhost:5000/api/categories/{categoryId}
        [Route("{categoryId:int}")]
        [HttpGet]
        public IActionResult GetCategoriesById(int categoryId)
        {
            return Ok(_categoryService.GetCategoriesById(categoryId));
        }

        //POST http://localhost:5000/api/categories/
        [Route("")]
        [HttpPost]
        public IActionResult NewCategory([FromBody] CategoryInputModel newCategory)
        {
            return Ok();
        }

        //PUT http://localhost:5000/api/categories/{categoryId}
        [Route("{categoryId:int}")]
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryInputModel updatedNewsItem)
        {
            return Ok();
        }
        
        //DELETE http://localhost:5000/api/categories/{categoryId}
        [Route("{categoryId:int}")]
        [HttpDelete]
        public IActionResult DeleteCategory([FromBody] CategoryInputModel deletedNewsItem)
        {
            return Ok();
        }
    }
}