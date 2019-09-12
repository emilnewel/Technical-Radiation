using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private AuthorService _authorService = new AuthorService();
        private NewsService _newsItemService = new NewsService();

        private AuthenticationServices _authService = new AuthenticationServices();

        //GET http://localhost:5000/api/authors
        [Route("", Name = "GetAuthors")]
        [HttpGet]
        public IActionResult GetAuthors()
        {
            return Ok(_authorService.GetAuthors());
        }

        //GET http://localhost:5000/api/authors/{authorId}
        [Route("{authorId:int}")]
        [HttpGet]
        public IActionResult GetAuthorsById(int authorId)
        {
            IEnumerable<AuthorDetailsDto> authors;
            try
            {
                authors = _authorService.GetAuthorsById(authorId);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }


            return Ok(authors);
        }

        //GET http://localhost:5000/api/authors/{authorId}/newsItem
        [Route("{authorId:int}/newsItems")]
        [HttpGet]
        public IActionResult GetNewsByAuthorId(int authorId)
        {
            IEnumerable<NewsItemDto> newsItems;
            try
            {
                newsItems = _authorService.GetNewsByAuthorId(authorId);
            }
            catch (ResourceNotFoundException ex)
            {

                return NotFound(ex.Message);
            }
            return Ok(newsItems);
        }

        //POST http://localhost:5000/api/authors
        [Route("")]
        [HttpPost]
        public IActionResult InsertAuthor([FromBody] AuthorInputModel newAuthor)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest("Model is not properly formatted.");

            int newId = _authorService.InsertAuthor(newAuthor);
            return CreatedAtRoute("GetAuthors", new { id = newId }, null);
        }

        //PUT http://localhost:5000/api/authors/{AuthorId}
        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateAuthor([FromBody] AuthorInputModel updatedAuthor, int id)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest("Model is not properly formatted.");

            try
            {
                _authorService.UpdateAuthor(updatedAuthor, id);
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        //DELETE http://localhost:5000/api/authors/{AuthorId}
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteAuthor(int id)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();

            try
            {
                _authorService.DeleteAuthor(id);
            }
            catch (ResourceNotFoundException ex)
            {

                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        //PUT http://localhost:5000/api/authors/{authorId}/newsItems/{newsItemId}
        [HttpPut]
        [Route("{id:int}/newsItems/{newItemId:int}")]
        public IActionResult LinkAuthor([FromBody] AuthorInputModel linkAuthor)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }
    }
}