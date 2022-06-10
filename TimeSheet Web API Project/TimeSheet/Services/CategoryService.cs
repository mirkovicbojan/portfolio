using TimeSheet.CustomExceptions;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        public IEnumerable<Category> GetAll()
        {
            var retVal = _categoryRepository.GetAll();

            if (retVal.Count() == 0)
            {
                throw new EmptyListException("There are no Categories currently in database");
            }

            return retVal;
        }

        public Category GetOne(int id)
        {
            var retVal = _categoryRepository.GetById(id);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Category with id: {id} not found.");
            }

            return retVal;
        }

        public void DeleteOne(int id)
        {
            var retVal = _categoryRepository.GetById(id);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Category with id: {id} not found.");
            }

            _categoryRepository.Delete(retVal);
        }

        public Category UpdateOne(Category obj)
        {
            var retVal = _categoryRepository.GetById(obj.categoryID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Category with id: {obj.categoryID} not found.");
            }

            return _categoryRepository.Edit(obj);
        }

        public Category Save(Category obj)
        {

            if (string.IsNullOrEmpty(obj.categoryName))
            {
                throw new InvalidObjectParamsException("Category name cannot be empty.");
            }

            return _categoryRepository.Save(obj);
        }
    }
}