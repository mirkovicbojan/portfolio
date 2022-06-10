using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Contexts;

namespace TimeSheet.Repository
{
    public class ReportRepository: Repository<TimeSheetClass>, IReportRepository
    {
        private TimeSheetContext dbContext;

        public ReportRepository(TimeSheetContext context):base(context)
        {
            dbContext = context;
        }
    }
}