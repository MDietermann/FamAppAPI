using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        #region Initialisierung

        private readonly ICalendarRepository _calendarRepository;
        private readonly IMapper _mapper;

        public CalendarController(ICalendarRepository calendarRepository, IMapper mapper)
        {
            _calendarRepository = calendarRepository;
            _mapper = mapper;
        }

        #endregion

        #region GET-Methoden

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Calendar>))]
        public IActionResult GetCalendars()
            => !ModelState.IsValid
            ? BadRequest(ModelState)
            : Ok(_mapper.Map<List<CalendarDto>>(_calendarRepository.GetCalendars()));

        [HttpGet("id/{groupId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Calendar>))]
        public IActionResult GetCalendarsByGroupId(int groupId)
            => !ModelState.IsValid
            ? BadRequest(ModelState)
            : Ok(_mapper.Map<List<CalendarDto>>(_calendarRepository.GetCalendarOfGroup(groupId)));

        #endregion

        #region POST-Methoden

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCalendar([FromBody] CalendarDto calendarCreate)
        {
            if (calendarCreate == null)
                return BadRequest(ModelState);

            var calendar = _calendarRepository.GetCalendars()
                .Where(c => c.id == calendarCreate.id)
                .FirstOrDefault();

            if (calendar != null)
            {
                ModelState.AddModelError("", "Calendar with this ID already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var calendarMap = _mapper.Map<Calendar>(calendarCreate);

            if (!_calendarRepository.CreateCalendar(calendarMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created calendar");

        }

        #endregion

        #region DELETE-Methoden

        [HttpDelete("{calendarId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCalendar(int calendarId)
        {
            if (!_calendarRepository.CalendarExists(calendarId))
                return NotFound();

            var calendarToDelete = _calendarRepository.GetCalendar(calendarId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_calendarRepository.DeleteCalendar(calendarToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        #endregion
    }
}
