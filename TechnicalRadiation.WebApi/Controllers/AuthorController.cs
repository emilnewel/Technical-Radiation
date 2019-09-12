using Microsoft.AspNetCore.Mvc;
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
        [Route("")]
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
            return Ok(_authorService.GetAuthorsById(authorId));
        }

        //GET http://localhost:5000/api/authors/{authorId}/newsItem
        [Route("{authorId:int}/newsItems")]
        [HttpGet]
        public IActionResult GetNewsByAuthorId(int authorId)
        {
            return Ok(_authorService.GetNewsByAuthorId(authorId));
        }

        //POST http://localhost:5000/api/authors
        [Route("")]
        [HttpPost]
        public IActionResult NewAuthor([FromBody] AuthorInputModel newAuthor)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }

        //PUT http://localhost:5000/api/authors/{AuthorId}
        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateAuthor([FromBody] AuthorInputModel updatedAuthor)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }
        
        //DELETE http://localhost:5000/api/authors/{AuthorId}
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteAuthor([FromBody] AuthorInputModel deletedAuthor)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }

        //PUT (/api/authors/{authorId}/newsItems/{newsItemId}
        [HttpPut]
        [Route("{id:int}/newsItems/{newItemId:int}")]
        public IActionResult LinkAuthor([FromBody] AuthorInputModel linkAuthor)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }
    }
}