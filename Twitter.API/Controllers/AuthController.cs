using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Twitter.Business.Dtos.AuthoDtos;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Core.Entities;

namespace Twitter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(IEmailService emailService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto vm)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Name = vm.Name,
                    Email = vm.Email,
                    UserName = vm.Username,
                    Surname = vm.Surname,
                    BirthDate = vm.BirthDate,
                };
                var result = await _userManager.CreateAsync(user, vm.Password);

                 _emailService.SendMail();
            }

            return Ok();
        }
    }
}
