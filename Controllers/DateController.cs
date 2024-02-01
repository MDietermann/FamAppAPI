using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamAppAPI.Controllers
{
    /// <summary>
    /// The date controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DateController : ControllerBase
    {
        #region Initialization

        /// <summary>
        /// The date repository.
        /// </summary>
        private readonly IDateRepository _dateRepository;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateController"/> class.
        /// </summary>
        /// <param name="dateRepository">The date repository.</param>
        /// <param name="mapper">The mapper.</param>
        public DateController(IDateRepository dateRepository, IMapper mapper)
        {
            _dateRepository = dateRepository;
            _mapper = mapper;
        }

        #endregion

        #region GET-Methods

        /// <summary>
        /// Gets the dates.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Date>))]
        public IActionResult GetDates()
            => !ModelState.IsValid
            ? BadRequest(ModelState)
            : Ok(_mapper.Map<List<DateDto>>(_dateRepository.GetDates()));

        /// <summary>
        /// Gets the dates by calendar id.
        /// </summary>
        /// <param name="calendarId">The calendar id.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("calendar/{calendarId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Date>))]
        [ProducesResponseType(404)]
        public IActionResult GetDatesByCalendarId(int calendarId)
            => !ModelState.IsValid
            ? NotFound()
            : Ok(_mapper.Map<List<DateDto>>(_dateRepository.GetDatesByCalendar(calendarId)));

        /// <summary>
        /// Gets the dates by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Date>))]
        [ProducesResponseType(404)]
        public IActionResult GetDatesByUserId(int userId)
            => !ModelState.IsValid
            ? NotFound()
            : Ok(_mapper.Map<List<DateDto>>(_dateRepository.GetDatesByUser(userId)));

        /// <summary>
        /// Gets the dates by user id and calendar id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="calendarId">The calendar id.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("user/{userId}/calendar/{calendarId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Date>))]
        [ProducesResponseType(404)]
        public IActionResult GetDatesByUserIdAndCalendarId(int userId, int calendarId)
            => (!_dateRepository.DateExistsCalendar(calendarId) || !_dateRepository.DateExistsUser(userId))
            ? NotFound()
            : Ok(_mapper.Map<List<DateDto>>(_dateRepository.GetDatesByUserAndCalendar(userId, calendarId)));

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Date))]
        [ProducesResponseType(404)]
        public IActionResult GetDate(int id)
            => !_dateRepository.DateExists(id)
            ? NotFound()
            : Ok(_mapper.Map<DateDto>(_dateRepository.GetDate(id)));

        #endregion

        #region POST-Methods

        /// <summary>
        /// Creates the date.
        /// </summary>
        /// <param name="dateCreate">The date create.</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDate([FromBody] DateDto dateCreate)
        {
            if (dateCreate == null)
                return BadRequest(ModelState);

            var date = _dateRepository.GetDates()
                .Where(d => d.id == dateCreate.id)
                .FirstOrDefault();

            if (date != null)
            {
                ModelState.AddModelError("", "Date with this ID already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dateMap = _mapper.Map<Date>(dateCreate);

            if (!_dateRepository.CreateDate(dateMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created date");
        }

        #endregion

        #region PUT-Methods

        /// <summary>
        /// Update date.
        /// </summary>
        /// <param name="dateId">The date id.</param>
        /// <param name="dateUpdate">The date update.</param>
        /// <returns>IActionResult</returns>
        [HttpPost("update/{dateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateDate(int dateId, [FromBody] DateDto dateUpdate)
        {
            if (dateUpdate == null)
                return BadRequest(ModelState);

            if (dateId != dateUpdate.id)
                return BadRequest(ModelState);

            if (!_dateRepository.DateExists(dateId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dateMap = _mapper.Map<Date>(dateUpdate);

            if (!_dateRepository.UpdateDate(dateMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion

        #region DELETE-Methods

        /// <summary>
        /// Deletes the date.
        /// </summary>
        /// <param name="dateId">The date id.</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{dateId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDate(int dateId)
        {
            if (!_dateRepository.DateExists(dateId))
                return NotFound();

            var dateToDelete = _dateRepository.GetDate(dateId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_dateRepository.DeleteDate(dateToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion
    }
}
