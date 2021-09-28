using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Data.Models;
using ProjectTest.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ProjectTest.Commons;
using ProjectTest.Data.Repositories.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace ProjectTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger; 
        private readonly IUserService UserService;
        private readonly IUserRepository UserRepository;

        public UserController(IUserService userService,
            IUserRepository userRepository,
            ILogger<UserController> logger)
        {
            UserService = userService;
            UserRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("StoreDataByFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Errors), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<string>>> StoreDataByFile()
        {
            try
            {
                var userService = await UserService.StoreData();

                if (userService.Any())
                    return Ok(userService);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("StoreDataByParameters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Errors), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<string>>> StoreDataByParameters(string[] users)
        {
            try
            {
                var userService = await UserService.StoreData(users);

                if (userService.Any())
                    return Ok(userService);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetFullData")]
        [ProducesResponseType(typeof(TblUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Errors), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetFullData()
        {
            try
            {
                return Ok(await UserRepository.GetData());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("FullProcessByFile")]
        [ProducesResponseType(typeof(TblUser), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Errors), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TblUser>>> FullProcessByFile()
        {
            try
            {
                var userService = await UserService.StoreData();

                if (userService.Any())
                    return Ok(await UserRepository.GetData());

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
