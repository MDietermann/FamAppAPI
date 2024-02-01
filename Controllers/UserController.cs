using AutoMapper;
using FamAppAPI.Dto;
using FamAppAPI.Interfaces;
using FamAppAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamAppAPI.Controllers
{
    // Controller für die Benutzerverwaltung
    /// <summary>
    /// The user controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Initialisierung
        /// <summary>
        /// The user repository.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;

        // Konstruktor zur Initialisierung des UserController
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="mapper">The mapper.</param>
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region GET-Methoden


        // Alle Benutzer abrufen
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
            => !ModelState.IsValid
            ? BadRequest(ModelState)
            : Ok(_mapper.Map<List<UserDto>>(_userRepository.GetUsers()));

        // Benutzer anhand der ID abrufen
        /// <summary>
        /// Gets the user by id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("id/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(int userId)
            => (!_userRepository.UserExistsById(userId))
            ? NotFound()
            : Ok(_mapper.Map<UserDto>(_userRepository.GetUserById(userId)));

        // Benutzer anhand der E-Mail-Adresse abrufen
        /// <summary>
        /// Gets the user by mail.
        /// </summary>
        /// <param name="userEmail">The user email.</param>
        /// <returns>IActionResult</returns>
        [HttpGet("mail/{userEmail}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(404)]
        public IActionResult GetUserByMail(string userEmail)
            => (!_userRepository.UserExistsByMail(userEmail))
            ? NotFound()
            : Ok(_mapper.Map<UserDto>(_userRepository.GetUserByMail(userEmail)));

        #endregion

        #region POST-Methoden

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userCreation">The user creation.</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userCreation)
        {
            // Check ob userCreation leer ist
            // Wenn ja -> BadRequest
            if (userCreation == null)
                return BadRequest(ModelState);

            // Sucht nach einer Benutzer mit dieser E-Mail
            var user = _userRepository.GetUsers()
                .Where(u => u.email.Trim().ToUpper() == userCreation.email.TrimEnd().ToUpper())
                .FirstOrDefault();

            // Wenn eine Benutzer mit dieser E-Mail existiert -> BadRequest
            if (user != null)
            {
                ModelState.AddModelError("", "User with this E-Mail already exists");
                return StatusCode(422, ModelState);
            }

            // Check ob userCreation valide ist
            // Wenn nein -> BadRequest
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Erstellt eine neue Benutzer
            var userMap = _mapper.Map<User>(userCreation);

            if (!_userRepository.CreateUser(userMap))
            {
                // Wenn der Benutzer nicht erstellt werden konnte -> ErrorCode
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created user");
        }

        #endregion

        #region PUT-Methoden

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="userUpdate">The user update.</param>
        /// <returns>IActionResult</returns>
        [HttpPut("update/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userUpdate)
        {
            // Check ob userUpdate leer ist
            // Wenn ja -> BadRequest
            if (userUpdate == null)
                return BadRequest(ModelState);

            // Check ob die gegebene ID mit der ID von userUpdate übereinstimmt
            // Wenn nicht -> BadRequest
            if (userId != userUpdate.id)
                return BadRequest(ModelState);

            // Check ob die Benutzer existiert
            // Wenn nicht -> NotFound
            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            // Check ob userUpdate valide ist
            // Wenn nicht -> BadRequest
            if (!ModelState.IsValid)
                return BadRequest();

            // Update
            var userMap = _mapper.Map<User>(userUpdate);

            if (!_userRepository.UpdateUser(userMap))
            {
                // Wenn der Benutzer nicht aktualisiert werden konnte -> ErrorCode
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        #endregion

        #region DELETE-Methoden

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            // Check ob der Benutzer existiert
            // Wenn nicht -> NotFound
            if (!_userRepository.UserExistsById(userId))
                return NotFound();

            // Sucht den Benutzer in der Datenbank
            var userToDelete = _userRepository.GetUserById(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        #endregion
    }
}