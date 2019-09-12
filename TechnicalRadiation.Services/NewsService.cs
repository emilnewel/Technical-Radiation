using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class NewsService
    {
        private NewsRepository _newsRepository = new NewsRepository();
        private AuthorRepository _authorRepository = new AuthorRepository();
        private CategoryRepository _categoryRepository = new CategoryRepository();

        public IEnumerable<NewsItemDto> GetAllNews()
        {
            var newsItems = _newsRepository.GetAllNews().ToList();
            newsItems.ForEach(ni => {
                ni.Links.AddReference("self", $"/api/{ni.Id}");
                ni.Links.AddReference("edit", $"/api/{ni.Id}");
                ni.Links.AddReference("delete", $"/api/{ni.Id}");
                ni.Links.AddListReference("authors", _authorRepository.GetAuthorsByNewsId(ni.Id).Select(a => new { href = $"/api/authors/{a.Id}"}));
                ni.Links.AddListReference("categories", _categoryRepository.GetCategoriesByNewsId(ni.Id).Select(c => new { href = $"/api/categories/{c.Id}"}));
                
            });

            return newsItems;
        }
        public IEnumerable<NewsItemDetailDto> GetNewsById(int Id) {
            var newsItem = _newsRepository.GetNewsById(Id).ToList();  
            newsItem.ForEach(ni => {
                ni.Links.AddReference("self", $"/api/{ni.Id}");
                ni.Links.AddReference("edit", $"/api/{ni.Id}");
                ni.Links.AddReference("delete", $"/api/{ni.Id}");
                ni.Links.AddListReference("authors", _authorRepository.GetAuthorsByNewsId(ni.Id).Select(a => new { href = $"/api/authors/{a.Id}"}));
                ni.Links.AddListReference("categories", _categoryRepository.GetCategoriesByNewsId(ni.Id).Select(c => new { href = $"/api/categories/{c.Id}"}));
            });
            return newsItem;
        }
        public int InsertNewsItem(NewsItemInputModel newNewsItem){
            return _newsRepository.InsertNewsItem(newNewsItem);
        }
        public void UpdateNewsItem(NewsItemInputModel newNewsItem, int id){
            _newsRepository.UpdateNewsItem(newNewsItem, id);
        }
        public void DeleteNewsById(int id){
            _newsRepository.DeleteNewsById(id);
        }
         
    }
}