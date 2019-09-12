using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using System.Linq;
using System;

namespace TechnicalRadiation.Repositories
{
    public class CategoryRepository
    {        
        public IEnumerable<CategoryDto> GetCategories()
        {
            return DataProvider.Categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug
            });
        }
        public IEnumerable<CategoryDto> GetCategoriesById(int Id)
        {
            return DataProvider.Categories.Where(c => c.Id == Id).Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Slug = c.Slug
            });
        }
        public int InsertCategory(CategoryInputModel newCategory){
            var nextId = DataProvider.Categories.OrderByDescending(n => n.Id).FirstOrDefault().Id + 1;
            var newThing = new Category
            {
                Id = nextId,
                Name = newCategory.Name,
                Slug = newCategory.Name.ToLower().Replace(' ','-'),
                ModifiedBy = "admin",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            DataProvider.Categories.Add(newThing);
            
            return nextId;
        }
    }
}