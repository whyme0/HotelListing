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
        public async Task<IActionResult> Register([FromBody] ApiUserDTO apiUser)
        {
            _logger.LogInformation($"Registration attempt with email {apiUser.Email}");
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            try
            {
                var user = _mapper.Map<ApiUser>(apiUser);
                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "User registration attempt failed");
                }
                return StatusCode(StatusCodes.Status201Created, $"User with email {apiUser.Email} created");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while proceeding request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }
    }
}
