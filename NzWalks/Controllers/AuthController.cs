using System;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Model.DTO;
using NzWalks.Repositories;

namespace NzWalks.Controllers
{   
    [ApiController]
    [Route("auth")]
    public class AuthController: Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest) {
            //Validate incoming request
            //Check if user is authenticated
            var user = await userRepository.AuthenticateUserAsync(loginRequest.Username, loginRequest.Password);

            if(user != null)
            {
                //Generate Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            } 

            return BadRequest("Username or password is incorrect");
            
        }
    }
}
