using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Data;
using ProductCatalog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly StoreDataContext _context;

        #region Constructor
        public UserController(IUserService userService, StoreDataContext context)
        {
            _userService = userService;
            _context = context;
        }
        #endregion

        #region GetUsers
        [HttpGet]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        //[Authorize(Roles = "manage")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        #endregion

        #region Get User by Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "manage")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var obj = await _userService.GetUserById(id);
            if (obj == null)
                return NotFound();

            return Ok(obj);
        }
        #endregion

        #region CreateUser
        [HttpPost]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] User model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var obj = await _userService.CreateUser(model);

                return Created(nameof(CreateUser), obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Authenticate
        [HttpPost("login")]
        public async Task<dynamic> Authenticate([FromBody] User model)
        {

            try
            {
                var user = await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .FirstOrDefaultAsync();

                if (user == null)
                    return NotFound(new { message = "Username or Password is invalid" });

                var token = TokenService.GenerateToken(user);

                return Ok(new
                {
                    token = token
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
