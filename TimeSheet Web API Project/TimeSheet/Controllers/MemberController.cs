using Microsoft.AspNetCore.Mvc;
using TimeSheet.Models;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_memberService.GetAll());
        }

        [HttpPost]
        public IActionResult Save(Member obj)
        {
            return Ok(_memberService.Save(obj));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var member = _memberService.GetOne(id);
            if (member == null)
            {
                return BadRequest("Member not found");
            }
            return Ok(member);
        }

        [HttpPut]
        public IActionResult UpdateMember(Member request)
        {
            var update = _memberService.GetOne(request.memberID);
            if (update == null)
            {
                return BadRequest("Member not found.");
            }
            return Ok(_memberService.UpdateOne(request));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = _memberService.GetOne(id);
            if (member == null)
            {
                return BadRequest("Member not found");
            }
            _memberService.DeleteOne(member);
            return Ok("Member deleted sucessfully");
        }
    }
}