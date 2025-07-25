using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMS.API.Services.Interfaces;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

    }
}
