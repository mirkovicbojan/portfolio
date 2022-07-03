using TimeSheet.Models;

namespace TimeSheet.Repository.Interfaces
{
    public interface IMemberRepository:IRepository<Member>
    {
        public Member findByCredentials(string credential);

    }
}