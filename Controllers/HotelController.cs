using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Units;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly ILogger<HotelController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitOfWork unit, ILogger<HotelController> logger, IMapper mapper)
        {
            _unit = unit;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                IList<Hotel> hotels = await _unit.Hotels.GetAll();
                var result = _mapper.Map<List<HotelDTO>>(hotels);
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
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                Hotel? hotel = await _unit.Hotels.Get(q => q.Id == id, new List<string> { "Country" });
                if (hotel != null)
                {
                    var result = _mapper.Map<HotelDTO>(hotel);
                    return Ok(result);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Hotel with such id was not found");
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
