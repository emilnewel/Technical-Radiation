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
    public class NewsRepository
    {
        public IEnumerable<NewsItemDto> GetAllNews() => DataProvider.NewsItems.Select(n => new NewsItemDto
        {
            Id = n.Id,
            Title = n.Title,
            ImgSource = n.ImgSource,
            ShortDescription = n.ShortDescription
        });

        public IEnumerable<NewsItemDetailDto> GetNewsById(int Id)
        {
            return DataProvider.NewsItems.Where(n => n.Id == Id).Select(n => new NewsItemDetailDto
            {
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription,
                LongDescription = n.LongDescription,
                PublishDate = n.PublishDate
            });
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int authorId)
        {
            var newsItemIds = DataProvider.newsAuthors.Where(x => x.AuthorId == authorId).ToList();
            var newsitems = new List<NewsItemDto>();

            foreach (var item in newsItemIds)
            {
                newsitems.Add(DataProvider.NewsItems.Where(x => x.Id == item.NewsItemId).Select(n => new NewsItemDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    ImgSource = n.ImgSource,
                    ShortDescription = n.ShortDescription
                }).FirstOrDefault());
            }

            return newsitems;
        }

        public int InsertNewsItem(NewsItemInputModel newsItem)
        {
            var nextId = DataProvider.NewsItems.OrderByDescending(n => n.Id).FirstOrDefault().Id + 1;
            var newThing = new NewsItem
            {
                Id = nextId,
                Title = newsItem.Title,
                ImgSource = newsItem.ImgSource,
                ShortDescription = newsItem.ShortDescription,
                LongDescription = newsItem.LongDescription,
                PublishDate = DateTime.Now,
                ModifiedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            DataProvider.NewsItems.Add(newThing);
            return nextId;
        }
        public void UpdateNewsItem(NewsItemInputModel updateItem, int id)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            if (entity == null) { throw new ResourceNotFoundException($"NewsItem with id {id} was not found."); }
            if (!string.IsNullOrEmpty(updateItem.Title)) entity.Title = updateItem.Title;
            if (!string.IsNullOrEmpty(updateItem.ImgSource)) entity.ImgSource = updateItem.ImgSource;
            if (!string.IsNullOrEmpty(updateItem.ShortDescription)) entity.ShortDescription = updateItem.ShortDescription;
            if (!string.IsNullOrEmpty(updateItem.LongDescription)) entity.LongDescription = updateItem.LongDescription;
            entity.ModifiedBy = "admin";
            entity.ModifiedDate = DateTime.Now;
        }
        public void DeleteNewsById(int id)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            if (entity == null) { throw new ResourceNotFoundException($"NewsItem with id {id} was not found."); }
            DataProvider.NewsItems.Remove(entity);
        }
        public int GetNumberOfNewsByCategoryId(int categoryId)
        {
            return DataProvider.newsItemCategories.Where(x => x.CategoryId == categoryId).Count();
        }
    }

}
