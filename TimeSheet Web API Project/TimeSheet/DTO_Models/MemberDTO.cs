using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.DTO_Models
{
    public class MemberDTO
    {
        public string? memberName { get; set; }

        public static MemberDTO memberToDTO(Member member)
        {
            return new MemberDTO
            {
                memberName = member.memberName
            };
        }
    }
}