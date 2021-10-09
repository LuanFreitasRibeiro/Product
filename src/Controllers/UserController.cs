using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Request.User;
using ProductCatalog.Domain.Response.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateService _authenticateService;

        #region Constructor
        public UserController(IUserService userService, IAuthenticateService authenticateService)
        {
            _userService = userService;
            _authenticateService = authenticateService;
        }
        #endregion

        #region GetUsers
        [HttpGet]
        [ProducesResponseType(typeof(GetUserResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        //[Authorize(Roles = "manage")]
        public async Task<IEnumerable<GetUserResponse>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        #endregion

        #region Get User by Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserResponse), 200)]
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
        [ProducesResponseType(typeof(CreateUserResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest model)
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
        public async Task<dynamic> Authenticate([FromBody] AuthenticateRequest model)
        {
            try
            {
                var auth = await _authenticateService.Authenticate(model);
                return Ok(new
                {
                    token = auth
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
