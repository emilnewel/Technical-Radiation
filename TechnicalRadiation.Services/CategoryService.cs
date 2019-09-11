using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;


namespace TechnicalRadiation.Services
{
    public class CategoryService
    {
        private CategoryRepository _categoryRepository = new CategoryRepository();

        public IEnumerable<CategoryDto> GetCategories() => _categoryRepository.GetCategories();
        public IEnumerable<CategoryDto> GetCategoriesById(int Id) => _categoryRepository.GetCategoriesById(Id);
    }
}

