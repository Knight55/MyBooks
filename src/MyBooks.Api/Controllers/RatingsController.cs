using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBooks.Bll.Services;
using MyBooks.Dto.Dtos;

namespace MyBooks.Api.Controllers
{
    /// <summary>
    /// Public API controllers for ratings.
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly ILogger<RatingsController> _logger;
        private readonly IMapper _mapper;
        private readonly IRatingService _ratingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The object mapper.</param>
        /// <param name="ratingService">The rating service.</param>
        public RatingsController(
            ILogger<RatingsController> logger,
            IMapper mapper,
            IRatingService ratingService)
        {
            _logger = logger;
            _mapper = mapper;
            _ratingService = ratingService;
        }

        /// <summary>
        /// Gets a specific rating with the given identifier.
        /// </summary>
        /// <param name="id">Identifier of the rating.</param>
        /// <returns>
        /// Returns a specific rating with the given identifier.
        /// </returns>
        /// <response code="200">Returns a specific rating with the given identifier.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Rating), (int)HttpStatusCode.OK)]
        public IActionResult Get(int id)
        {
            return Ok(_mapper.Map<Rating>(_ratingService.GetRating(id)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Rating>), (int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery]int bookId, [FromQuery]int from, [FromQuery]int to)
        {
            // TODO: get ratings for the given book (from and to for paging)
            return null;
        }
    }
}