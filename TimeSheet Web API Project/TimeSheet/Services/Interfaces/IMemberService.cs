using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces
{
    public interface IMemberService
    {
        public IEnumerable<Member> GetAll();

        public Member GetOne(int id);

        public void DeleteOne(Member obj);

        public Member UpdateOne(Member obj);

        public Member Save(Member obj);
    }
}