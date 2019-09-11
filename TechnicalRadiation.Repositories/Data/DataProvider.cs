using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.Repositories.Data
{
    public static class DataProvider
    {
        private static readonly string _adminName = "Admin";
        public static List<Author> Authors = new List<Author>
        {
            new Author 
            {
                Id = 1,
                Name = "Emil Newel",
                ProfileImgSource = "webimgurlyes.jpg",
                Bio = "polskur kall",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now 

            },
            new Author 
            {
                Id = 2,
                Name = "Maggi pulsa",
                ProfileImgSource = "webimgurlyes2.jpg",
                Bio = "lítill kall",
                ModifiedBy = _adminName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now 

            }
        };

        public static List<Category> Categories = new List<Category>
        {
            new Category 
            {
                Id = 1,
                Name = "Action news",
                Slug = "hvað er slug aftur?",
                ModifiedBy = _adminName,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now

            },
            new Category 
            {
                Id = 2,
                Name = "No Action news",
                Slug = "hvað er slug aftur?",
                ModifiedBy = _adminName,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now

            }
        };
        public static List<NewsItem> NewsItems = new List<NewsItem>
        {
            new NewsItem 
            {
                Id = 1,
                Title = "sprengja búmbúm",
                ImgSource = "myndainternetinu.jpg",
                ShortDescription = "sprengja",
                LongDescription = "Sprengja sprakk",
                PublishDate = DateTime.Now,
                ModifiedBy = _adminName,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now

            },
            new NewsItem 
            {
                Id = 2,
                Title = "´Instagramkona á hús",
                ImgSource = "myndainternetinu.jpg",
                ShortDescription = "hús",
                LongDescription = "Hvernig borgaði hun fyrir húsið",
                PublishDate = DateTime.Now,
                ModifiedBy = _adminName,
                ModifiedDate = DateTime.Now,
                CreatedDate = DateTime.Now

            }
        };
        public static List<NewsItemAuthors> newsAuthors = new List<NewsItemAuthors>
        {
            new NewsItemAuthors 
            {
                
                AuthorId = 1,
                NewsItemId = 1
            },
            new NewsItemAuthors 
            {
                
                AuthorId = 2,
                NewsItemId = 2
            }
        };
        public static List<NewsItemCategories> newsItemCategories = new List<NewsItemCategories>
        {
            new NewsItemCategories
            {
                NewsItemId = 1,
                CategoryId = 1
            },
            new NewsItemCategories
            {
                NewsItemId = 2,
                CategoryId = 2
            }
        };
    }
}