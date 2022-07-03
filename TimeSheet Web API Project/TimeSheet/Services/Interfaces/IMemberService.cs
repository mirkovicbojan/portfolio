using TimeSheet.DTO_Models;
using TimeSheet.Models;

namespace TimeSheet.Services.Interfaces
{
    public interface IMemberService
    {
        public IEnumerable<MemberDTO> GetAll();

        public Member GetOne(int id);

        public void DeleteOne(Member obj);

        public Member UpdateOne(Member obj);

        public Member Save(Member obj);

        
    }
}