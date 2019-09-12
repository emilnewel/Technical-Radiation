using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Exceptions;
using TechnicalRadiation.Models.HyperMedia;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories;


namespace TechnicalRadiation.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository = new CategoryRepository();

        public IEnumerable<CategoryDto> GetCategories()
        {

            var categories = _categoryRepository.GetCategories().ToList();
            categories.ForEach(c =>
            {
                c.Links.AddReference("self", $"/api/categories/{c.Id}");
                c.Links.AddReference("edit", $"/api/categories/{c.Id}");
                c.Links.AddReference("delete", $"/api/categories/{c.Id}");
            });

            return categories;

        }

        public IEnumerable<CategoryDetailDto> GetCategoriesById(int Id)
        {
            var categories = _categoryRepository.GetCategoriesById(Id).ToList();
            if (categories == null) { throw new ResourceNotFoundException($"Category with id {Id} was not found."); }
            categories.ForEach(c =>
            {
                c.Links.AddReference("self", $"/api/categories/{c.Id}");
                c.Links.AddReference("edit", $"/api/categories/{c.Id}");
                c.Links.AddReference("delete", $"/api/categories/{c.Id}");
            });

            return categories;
        }

        public int InsertCategory(CategoryInputModel newCategory)
        {
            return _categoryRepository.InsertCategory(newCategory);
        }

        public void UpdateCategory(CategoryInputModel updatedCategory, int id)
        {
            _categoryRepository.UpdateCategory(updatedCategory, id);
        }

        public void DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategories(id);
        }
    }
}

