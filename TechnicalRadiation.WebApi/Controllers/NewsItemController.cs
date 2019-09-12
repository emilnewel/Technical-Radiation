using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechnicalRadiation.Models.Dtos;
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
        [Route("")]
        [HttpGet]
        public IActionResult GetAllNews([FromQuery] int pageSize = 25, [FromQuery] int pageNumber = 1)
        {
            IEnumerable<NewsItemDto> allNewsItems = _newsService.GetAllNews(); 
            var envelope = new Envelope<NewsItemDto>(pageSize, pageNumber, allNewsItems); //Vantar gögnin
            
            envelope.Items = allNewsItems.ToList().Skip((pageNumber-1) * pageSize).Take(pageSize);
            envelope.MaxPages = (int) Math.Ceiling(allNewsItems.Count() / (decimal) pageSize);
            
            return Ok(envelope);
        }       

        //GET http://localhost:5000/api/{newsItemId}
        [Route("{id:int}")]
        [HttpGet]
        public IActionResult GetNewsById(int id)
        {
            return Ok(_newsService.GetNewsById(id));
        }

        //POST http://localhost:5000/api/
        [Route("")]
        [HttpPost]
        public IActionResult NewNewsItem([FromBody] NewsItemInputModel newNewsItem)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }

        //PUT http://localhost:5000/api/{newsItemId}
        [Route("{id:int}")]
        [HttpPut]
        public IActionResult UpdateNewsItem([FromBody] NewsItemInputModel updatedNewsItem)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }
        
        //DELETE http://localhost:5000/api/{newsItemId}
        [Route("{id:int}")]
        [HttpDelete]
        public IActionResult DeleteNewsItem([FromBody] NewsItemInputModel deletedNewsItem)
        {
            if(!_authService.Validate(Request.Headers["Authorization"])) return Unauthorized();
            return Ok();
        }
    }
}
