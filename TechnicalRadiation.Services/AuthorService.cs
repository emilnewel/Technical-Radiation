using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;


namespace TechnicalRadiation.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository = new AuthorRepository();
        private NewsRepository _newsRepository = new NewsRepository();
        public IEnumerable<AuthorDto> GetAuthors() => _authorRepository.GetAuthors();
        public IEnumerable<AuthorDto> GetAuthorsById(int Id) => _authorRepository.GetAuthorsById(Id);
        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int Id) => _authorRepository.GetNewsByAuthorId(Id);
   }
}