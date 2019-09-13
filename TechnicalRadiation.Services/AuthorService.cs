using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;


namespace TechnicalRadiation.Services
{
    public class AuthorService
    {
        private AuthorRepository _authorRepository = new AuthorRepository();
        private NewsRepository _newsItemRepository = new NewsRepository();
        private CategoryRepository _categoryRepository = new CategoryRepository();
        public IEnumerable<AuthorDto> GetAuthors()
        {
            var authors = _authorRepository.GetAuthors().ToList();
            authors.ForEach(a =>
            {
                a.Links.AddReference("self", $"/api/authors/{a.Id}");
                a.Links.AddReference("edit", $"/api/authors/{a.Id}");
                a.Links.AddReference("delete", $"/api/authors/{a.Id}");
                a.Links.AddReference("newsItems", $"/api/authors/{a.Id}/newsItems");
                a.Links.AddListReference("newsItemDetailed", _newsItemRepository.GetNewsByAuthorId(a.Id).Select(n => new { href = $"/api/{n.Id}" }));
            });

            return authors;
        }
        public IEnumerable<AuthorDetailsDto> GetAuthorsById(int Id)
        {

            var authors = _authorRepository.GetAuthorsById(Id).ToList();
            if (authors == null) { throw new ResourceNotFoundException($"Author with id {Id} was not found."); }
            authors.ForEach(a =>
            {
                a.Links.AddReference("self", $"/api/authors/{a.Id}");
                a.Links.AddReference("edit", $"/api/authors/{a.Id}");
                a.Links.AddReference("delete", $"/api/authors/{a.Id}");
                a.Links.AddReference("newsItems", $"/api/authors/{a.Id}/newsItems");
                a.Links.AddListReference("newsItemsDetailed", _newsItemRepository.GetNewsByAuthorId(a.Id).Select(n => new { href = $"/api/{n.Id}" }));
            });

            return authors;

        }
        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int Id)
        {
            var news = _newsItemRepository.GetNewsByAuthorId(Id).ToList();
            if (news == null) { throw new ResourceNotFoundException($"Author with id {Id} was not found."); }
            news.ForEach(n =>
            {
                n.Links.AddReference("self", $"/api/{n.Id}");
                n.Links.AddReference("edit", $"/api/{n.Id}");
                n.Links.AddReference("delete", $"/api/{n.Id}");
                n.Links.AddListReference("authors", _authorRepository.GetAuthorsByNewsId(n.Id).Select(a => new { href = $"/api/authors/{a.Id}" }));
                n.Links.AddListReference("categories", _categoryRepository.GetCategoriesByNewsId(n.Id).Select(c => new { href = $"/api/categories/{c.Id}" }));
            });

            return news;
        }

        public int InsertAuthor(AuthorInputModel newAuthor)
        {
            return _authorRepository.InsertAuthor(newAuthor);
        }

        public void UpdateAuthor(AuthorInputModel updatedAuthor, int id)
        {
            _authorRepository.UpdateAuthor(updatedAuthor, id);
        }

        public void DeleteAuthor(int id)
        {
            _authorRepository.DeleteAuthor(id);
        }
        public void LinkAuthor(int authorId, int newsItemId){
            _authorRepository.LinkAuthor(authorId, newsItemId);
        }
    }
}