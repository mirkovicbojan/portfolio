using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces
{
    public interface ICategoryService
    {
        public IEnumerable<Category> GetAll();

        public Category GetOne(int id);

        public void DeleteOne(int id);

        public Category UpdateOne(Category obj);

        public Category Save(Category obj);
    }
}