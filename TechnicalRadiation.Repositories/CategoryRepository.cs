using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using System.Linq;
using System;
using TechnicalRadiation.Models.Exceptions;

namespace TechnicalRadiation.Repositories
{
    public class CategoryRepository
    {
        private NewsRepository _newsRepository = new NewsRepository();
        public IEnumerable<CategoryDto> GetCategories()
        {
            return DataProvider.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug
            });
        }
        public IEnumerable<CategoryDetailDto> GetCategoriesById(int Id)
        {
            //int numberofNewsItems = _newsRepository.G
            return DataProvider.Categories.Where(c => c.Id == Id).Select(c => new CategoryDetailDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug,
                NumberOfNewsItems = _newsRepository.GetNumberOfNewsByCategoryId(Id)
            });
        }
        public int InsertCategory(CategoryInputModel newCategory)
        {
            var nextId = DataProvider.Categories.OrderByDescending(n => n.Id).FirstOrDefault().Id + 1;
            var newThing = new Category
            {
                Id = nextId,
                Name = newCategory.Name,
                Slug = newCategory.Name.ToLower().Replace(' ', '-'),
                ModifiedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            DataProvider.Categories.Add(newThing);

            return nextId;
        }

        public void UpdateCategory(CategoryInputModel updatedCategory, int id)
        {
            var entity = DataProvider.Categories.FirstOrDefault(c => c.Id == id);
            if (entity == null) { throw new ResourceNotFoundException($"Category with id {id} was not found."); }
            if (!string.IsNullOrEmpty(updatedCategory.Name)) entity.Name = updatedCategory.Name;
            entity.ModifiedBy = "admin";
            entity.ModifiedDate = DateTime.Now;
        }

        public void DeleteCategories(int id)
        {
            var entity = DataProvider.Categories.FirstOrDefault(c => c.Id == id);
            if (entity == null) { throw new ResourceNotFoundException($"Category with id {id} was not found."); }
            DataProvider.Categories.Remove(entity);
        }
        public IEnumerable<CategoryDto> GetCategoriesByNewsId(int newsId)
        {
            var categoryIds = DataProvider.newsItemCategories.Where(x => x.NewsItemId == newsId).ToList();
            var categories = new List<CategoryDto>();

            foreach (var item in categoryIds)
            {
                categories.Add(DataProvider.Authors.Where(x => x.Id == item.CategoryId).Select(n => new CategoryDto
                {
                    Id = n.Id,
                    Name = n.Name
                }).FirstOrDefault());
            }

            return categories;
        }
        public void LinkCategory(int categoryId, int newsItemId){
            if(DataProvider.Categories.FirstOrDefault(n => n.Id == categoryId) == null)  {throw new ResourceNotFoundException($"Category with id {categoryId} was not found."); }        
            if(DataProvider.NewsItems.FirstOrDefault(n => n.Id == newsItemId) == null) {throw new ResourceNotFoundException($"News item with id {newsItemId} was not found."); }
            var newLink = new NewsItemCategories
            {
                CategoryId = categoryId,
                NewsItemId = newsItemId
            };
            DataProvider.newsItemCategories.Add(newLink);
        }
    }
}