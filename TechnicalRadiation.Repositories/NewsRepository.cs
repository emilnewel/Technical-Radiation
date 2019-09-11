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

        public IEnumerable<NewsItemDto> GetNewsById(int Id)
        {
            return DataProvider.NewsItems.Where(n => n.Id == Id).Select(n => new NewsItemDto
            {
                Id = n.Id,
                Title = n.Title,
                ImgSource = n.ImgSource,
                ShortDescription = n.ShortDescription
            });
        }

        // public IEnumerable<NewsItemDto> GetNewsByAuthorId(int authorId)
        // {
        //    Mátt endilega bomba í þettaa fall :p
        //     
        // }


    }

}
