using TimeSheet.CustomExceptions;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services.Interfaces;


namespace TimeSheet.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IEnumerable<Member> GetAll()
        {
            var retVal = _memberRepository.GetAll();

            if (retVal.Count() == 0)
            {
                throw new EmptyListException("No Members were found in database.");
            }

            return retVal;
        }

        public Member GetOne(int id)
        {
            var retVal = _memberRepository.GetById(id);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Member with id: {id} wasn't found.");
            }

            return retVal;
        }

        public void DeleteOne(Member obj)
        {
            var retVal = _memberRepository.GetById(obj.memberID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Member with id: {obj.memberID} wasn't found.");
            }

            _memberRepository.Delete(obj);
        }

        public Member UpdateOne(Member obj)
        {
            var retVal = _memberRepository.GetById(obj.memberID);

            if (retVal == null)
            {
                throw new KeyNotFoundException($"Member with id: {obj.memberID} wasn't found.");
            }

            return _memberRepository.Edit(obj);
        }

        public Member Save(Member obj)
        {
            
            if (string.IsNullOrEmpty(obj.memberName))
            {
                throw new InvalidObjectParamsException("Member name cannot be empty.");
            }

            return _memberRepository.Save(obj);
        }
    }
}