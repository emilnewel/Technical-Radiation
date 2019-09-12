using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Services;

namespace TechnicalRadiation.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class NewsItemController : ControllerBase
    {
        private NewsService _newsService = new NewsService();

        private AuthenticationServices _authService = new AuthenticationServices();


        //GET http://localhost:5000/api/
        [Route("", Name = "GetAllNews")]
        [HttpGet]
        public IActionResult GetAllNews([FromQuery] int pageSize = 25, [FromQuery] int pageNumber = 1)
        {
            IEnumerable<NewsItemDto> allNewsItems = _newsService.GetAllNews();
            var envelope = new Envelope<NewsItemDto>(pageSize, pageNumber, allNewsItems); //Vantar gögnin

            envelope.Items = allNewsItems.ToList().Skip((pageNumber - 1) * pageSize).Take(pageSize);
            envelope.MaxPages = (int)Math.Ceiling(allNewsItems.Count() / (decimal)pageSize);

            return Ok(envelope);
        }

        //GET http://localhost:5000/api/{newsItemId}
        [Route("{id:int}", Name = "GetNewsById")]
        [HttpGet]
        public IActionResult GetNewsById(int id)
        {
            return Ok(_newsService.GetNewsById(id));
        }

        //POST http://localhost:5000/api/
        [Route("")]
        [HttpPost]
        public IActionResult InsertNewsItem([FromBody] NewsItemInputModel newNewsItem)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            if (!ModelState.IsValid) { return BadRequest("Model is not properly formatted."); }

            var newId = _newsService.InsertNewsItem(newNewsItem);

            return CreatedAtRoute("GetNewsById", new { Id = newId }, null);
        }

        //PUT http://localhost:5000/api/{newsItemId}
        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateNewsItem([FromBody] NewsItemInputModel updatedNewsItem, [FromBody] int id)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            if (!ModelState.IsValid) { return BadRequest("Model is not properly formatted."); }

            _newsService.UpdateNewsItem(updatedNewsItem, id);
            return NoContent();
        }

        //DELETE http://localhost:5000/api/{newsItemId}
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteNewsItem([FromBody] int id)
        {
            if (!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();

            try
            {
                _newsService.DeleteNewsById(id);
            }
            catch (ResourceNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();

        }
    }
}
