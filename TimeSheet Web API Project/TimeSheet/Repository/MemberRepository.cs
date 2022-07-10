using TimeSheet.Contexts;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;

namespace TimeSheet.Repository
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        private TimeSheetContext dbContext;

        public MemberRepository(TimeSheetContext context) : base(context)
        {
            dbContext = context;
        }

        public Member findByCredentials(string credential)
        {
            var member = dbContext.Set<Member>().Where(m => m.email == credential).FirstOrDefault();
            if(member == null){
                throw new KeyNotFoundException("The entered user doesn't exist.");
            }
            return member ?? null;
        }
    }
}