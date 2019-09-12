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
        public IEnumerable<AuthorDetailsDto> GetAuthorsById(int Id)
        {
            return DataProvider.Authors.Where(c => c.Id == Id).Select(c => new AuthorDetailsDto
            {
                Id = c.Id,
                Name = c.Name,
                ProfileImgSource = c.ProfileImgSource,
                Bio = c.Bio
            });
        }
        public IEnumerable<AuthorDto> GetAuthorsByNewsId(int newsId)
        {
            var authorIds = DataProvider.newsAuthors.Where(x => x.NewsItemId == newsId).ToList();
            var authors = new List<AuthorDto>();

            foreach(var item in authorIds)
            {
                authors.Add(DataProvider.Authors.Where(x => x.Id == item.AuthorId).Select(n => new AuthorDto{
                    Id = n.Id,
                    Name = n.Name
                }).FirstOrDefault());
            }
            
            return authors;
        }
    }
}