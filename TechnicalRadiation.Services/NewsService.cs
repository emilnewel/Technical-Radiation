using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class NewsService
    {
        private NewsRepository _newsRepository = new NewsRepository();
        public IEnumerable<NewsItemDto> GetAllNews() => _newsRepository.GetAllNews();
        public IEnumerable<NewsItemDto> GetNewsById(int Id) => _newsRepository.GetNewsById(Id);
    }
}