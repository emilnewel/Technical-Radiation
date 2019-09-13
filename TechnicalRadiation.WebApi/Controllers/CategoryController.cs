using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService = new CategoryService();
        private AuthenticationServices _authService = new AuthenticationServices();

        //GET http://localhost:5000/api/categories
        [Route("", Name = "GetCategories")]
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
            IEnumerable<CategoryDetailDto> categories;
            try
            {
                categories = _categoryService.GetCategoriesById(categoryId);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(categories);
        }

        //POST http://localhost:5000/api/categories/
        [Route("")]
        [HttpPost]
        public IActionResult NewCategory([FromBody] CategoryInputModel newCategory)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            if (!ModelState.IsValid) { return BadRequest("Model is not properly formatted."); }

            var newId = _categoryService.InsertCategory(newCategory);
            return CreatedAtRoute("GetCategories", new { Id = newId }, null);
        }

        //PUT http://localhost:5000/api/categories/{categoryId}
        [Route("{categoryId:int}")]
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] CategoryInputModel updatedCategory, int id)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest("Model is not properly formatted.");

            try
            {
                _categoryService.UpdateCategory(updatedCategory, id);
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        //DELETE http://localhost:5000/api/categories/{categoryId}
        [Route("{categoryId:int}")]
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();

            try
            {
                _categoryService.DeleteCategory(id);
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
        //(/api/categories/{categoryId}/newsItems/{newsItemId}
        [Route("{categoryId:int}/newsItems/{newsItemId:int}")]
        [HttpPost]
        public IActionResult LinkCategory(int categoryId,int newsItemId)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            try
            {
                _categoryService.LinkCategory(categoryId, newsItemId);
            }
            catch (ResourceNotFoundException ex)
            {
                
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}