using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
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

            foreach(var item in newsItemIds)
            {
                newsitems.Add(DataProvider.NewsItems.Where(x => x.Id == item.NewsItemId).Select(n => new NewsItemDto{
                    Id = n.Id,
                    Title = n.Title,
                    ImgSource = n.ImgSource,
                    ShortDescription = n.ShortDescription
                }).FirstOrDefault());
            }
            
            return newsitems;
        }
    }

}
