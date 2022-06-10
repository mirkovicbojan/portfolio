using TimeSheet.Contexts;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class TimeSheetRepository:Repository<TimeSheetClass>, ITimeSheetRepository
    {
        private TimeSheetContext dbContext;

        public TimeSheetRepository(TimeSheetContext context):base(context)
        {
            dbContext = context;
        }
    }
}