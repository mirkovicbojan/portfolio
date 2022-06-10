using TimeSheet.Contexts;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class CategoryRepository:Repository<Category>, ICategoryRepository
    {
        private TimeSheetContext dbContext;
        

        public CategoryRepository(TimeSheetContext context):base(context)
        {
            dbContext = context;
        }
    }
}