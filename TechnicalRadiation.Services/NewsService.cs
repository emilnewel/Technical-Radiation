using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class NewsService
    {
        private NewsRepository _newsRepository = new NewsRepository();

        public IEnumerable<NewsItemDto> GetAllNews()
        {
            var newsItems = _newsRepository.GetAllNews().ToList();
            newsItems.ForEach(ni => {
                ni.Links.AddReference("self", $"/api/{ni.Id}");
                ni.Links.AddReference("edit", $"/api/{ni.Id}");
                ni.Links.AddReference("delete", $"/api/{ni.Id}");
                // authors ni.Links.AddReferenceList();
                // categories ni.Links.AddReferenceList();
                
            });

            return newsItems;
        }
        public IEnumerable<NewsItemDetailDto> GetNewsById(int Id) {
            var newsItem = _newsRepository.GetNewsById(Id).ToList();  
            newsItem.ForEach(ni => {
                ni.Links.AddReference("self", $"/api/{ni.Id}");
                ni.Links.AddReference("edit", $"/api/{ni.Id}");
                ni.Links.AddReference("delete", $"/api/{ni.Id}");
                // authors ni.Links.AddReferenceList();
                // categories ni.Links.AddReferenceList();
            });
            return newsItem;
        } 
    }
}