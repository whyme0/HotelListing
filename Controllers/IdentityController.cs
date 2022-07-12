using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;

        public IdentityController(ILogger<IdentityController> logger, IMapper mapper, UserManager<ApiUser> userManager, SignInManager<ApiUser> signInManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("signup/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody] RegistrationApiUserDTO apiUser)
        {
            _logger.LogInformation($"Registration attempt with email {apiUser.Email}");
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(apiUser);
                user.UserName = user.Email.Split("@")[0];
                var result = await _userManager.CreateAsync(user, apiUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
                }
                await _userManager.AddToRoleAsync(user, "User");
                return StatusCode(StatusCodes.Status201Created, $"User with email {apiUser.Email} created");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while proceeding request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }


        [HttpPost]
        [Route("signin/")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignIn([FromBody] LoginApiUserDTO apiUser)
        {
            _logger.LogInformation($"Login attempt with email {apiUser.Email}");
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            try
            {
                var result = await _signInManager.PasswordSignInAsync(apiUser.Email, apiUser.Password, false, false);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "User login attempt failed");
                }
                return StatusCode(StatusCodes.Status202Accepted, "Welcome!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while proceeding request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }
    }
}
