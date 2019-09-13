using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.InputModels;
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

            foreach (var item in authorIds)
            {
                authors.Add(DataProvider.Authors.Where(x => x.Id == item.AuthorId).Select(n => new AuthorDto
                {
                    Id = n.Id,
                    Name = n.Name
                }).FirstOrDefault());
            }

            return authors;
        }

        public int InsertAuthor(AuthorInputModel newAuthor)
        {
            var nextId = DataProvider.Authors.OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;
            var entity = new Author()
            {
                Id = nextId,
                Name = newAuthor.Name,
                ProfileImgSource = newAuthor.ProfileImgSource,
                Bio = newAuthor.Bio
            };

            DataProvider.Authors.Add(entity);
            return nextId;
        }

        public void UpdateAuthor(AuthorInputModel updatedAuthor, int id)
        {
            var entity = DataProvider.Authors.FirstOrDefault(a => a.Id == id);

            if (!string.IsNullOrEmpty(updatedAuthor.Name)) entity.Name = updatedAuthor.Name;
            if (!string.IsNullOrEmpty(updatedAuthor.Bio)) entity.Bio = updatedAuthor.Bio;
            if (!string.IsNullOrEmpty(updatedAuthor.ProfileImgSource)) entity.ProfileImgSource = updatedAuthor.ProfileImgSource;
            entity.ModifiedBy = "admin";
            entity.ModifiedDate = DateTime.Now;

        }

        public void DeleteAuthor(int id)
        {
            var entity = DataProvider.Authors.FirstOrDefault(a => a.Id == id);
            if (entity == null) { throw new ResourceNotFoundException($"Author with id {id} was not found."); }

            DataProvider.Authors.Remove(entity);
        }
        public void LinkAuthor(int authorId, int newsItemId){
            
            if(DataProvider.Authors.FirstOrDefault(a => a.Id == authorId) == null) {throw new ResourceNotFoundException($"Author with id {authorId} was not found."); }
            if(DataProvider.NewsItems.FirstOrDefault(n => n.Id == newsItemId) == null) {throw new ResourceNotFoundException($"News item with id {newsItemId} was not found."); }
            var newLink = new NewsItemAuthors 
            {
             AuthorId = authorId,
             NewsItemId = newsItemId   
            };
            DataProvider.newsAuthors.Add(newLink);
        }
    }
}