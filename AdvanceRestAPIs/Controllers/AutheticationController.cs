using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvanceRestAPIs.Data;
using AdvanceRestAPIs.Dtos.User;
using AdvanceRestAPIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceRestAPIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutheticationController : ControllerBase
    {

        public readonly IAuthRepository _authrepo;
        public AutheticationController(IAuthRepository authRepository)
        {
            _authrepo = authRepository;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto request)
        {

            ServiceResponse<int> response = await _authrepo.Register(
                new User { Username = request.Username }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            ServiceResponse<string> response = await _authrepo.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
