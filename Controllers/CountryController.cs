using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelListing.Units;
using HotelListing.Data;
using AutoMapper;
using HotelListing.Models;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitOfWork unit, ILogger<CountryController> logger, IMapper mapper)
        {
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                IList<Country> countries = await _unit.Countries.GetAll();
                var result = _mapper.Map<List<CountryDTO>>(countries);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while proceeding request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                Country? country = await _unit.Countries.Get(q => q.Id == id, new List<string> { "Hotels" });
                if (country != null)
                {
                    var result = _mapper.Map<CountryDTO>(country);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Country with such id was not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while proceeding request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error. Please try again later.");
            }
        }
    }
}
