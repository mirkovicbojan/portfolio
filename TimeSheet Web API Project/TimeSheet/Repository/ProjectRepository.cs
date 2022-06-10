using TimeSheet.Contexts;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class ProjectRepository:Repository<Project>, IProjectRepository
    {
        private TimeSheetContext dbContext;

        public ProjectRepository(TimeSheetContext context):base(context)
        {
            dbContext = context;
        }
    }
}