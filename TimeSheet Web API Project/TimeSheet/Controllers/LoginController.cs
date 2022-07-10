using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeSheet.DTO_Models;
using TimeSheet.Services;
using TimeSheet.Services.Interfaces;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : Controller
    {
        private readonly JwtAuthenticationService _authService;
        private IMemberService _memberService;
        public LoginController(JwtAuthenticationService authService, IMemberService memberService)
        {
            _authService = authService;
            _memberService = memberService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogIn([FromBody]MemberLoginDTO member)
        {
            var login = _memberService.CredentialCheck(member.email, member.password);
            var token = _authService.Authenticate(login.email, login.password);
            return Ok(token);
        }
    }
}