using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;


namespace TechnicalRadiation.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository = new CategoryRepository();

        public IEnumerable<CategoryDto> GetCategories() {
        
            var categories = _categoryRepository.GetCategories().ToList();
            categories.ForEach(c => {
                c.Links.AddReference("self", $"/api/categories/{c.Id}");
                c.Links.AddReference("edit", $"/api/categories/{c.Id}");
                c.Links.AddReference("delete", $"/api/categories/{c.Id}");
                c.Links.AddReference("newsItem", $"/api/categories/{c.Id}");
            });

            return categories;
            
        }
        public IEnumerable<CategoryDto> GetCategoriesById(int Id) => _categoryRepository.GetCategoriesById(Id);
        public int InsertCategory(CategoryInputModel newCategory){
            return _categoryRepository.InsertCategory(newCategory);
        }
    }
}

