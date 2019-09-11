using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories.Data;


namespace TechnicalRadiation.Repositories
{
    public class AuthorRepository
    {
        public IEnumerable<AuthorDto> GetAuthors()
        {
            return DataProvider.Authors.Select(c => new AuthorDto
            {
                Id = c.Id,
                Name = c.Name
            });   
        }
        public IEnumerable<AuthorDto> GetAuthorsById(int Id)
        {
            return DataProvider.Authors.Where(c => c.Id == Id).Select(c => new AuthorDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }
        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int Id)
        {
            return DataProvider.newsAuthors.Where(c => c.AuthorId == Id).Select(c => new NewsItemDto
            {
                Id = c.AuthorId
            });
        }
    }
}