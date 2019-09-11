using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Models.Entities;
using System.Linq;

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
    }
}