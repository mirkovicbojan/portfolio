using TimeSheet.Contexts;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class MemberRepository:Repository<Member>, IMemberRepository
    {
        private TimeSheetContext dbContext;

        public MemberRepository(TimeSheetContext context):base(context)
        {
            dbContext = context;
        }
    }
}