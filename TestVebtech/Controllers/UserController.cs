using Microsoft.AspNetCore.Mvc;
using TestVebtech.Models;
using TestVebtech.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestVebtech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Add new role to user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roleId">Role id</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/User/userId/roleId
        ///     {
        ///        "userId": 1, 
        ///        "roleId": 1
        ///     }
        /// </remarks>
        [HttpPost("{userId:int}/{roleId:int}")]
        public async Task<ActionResult<User>> AddNewRoleToUser(int userId, int roleId)
        {
            try
            {
                _logger.LogInformation("Adding a new role to the user userId - {userId}, roleId - {roleId}", userId, roleId);
                var success = await _userService.AddRoleToUser(userId, roleId);
                if (success)
                {
                    _logger.LogInformation("Role was added to user");

                    return Ok(success);
                }
                _logger.LogInformation("User or role not found");

                return NotFound("User or role not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal error 500: {ex.Message}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error add new role to user record");
            }
        }

        /// <summary>
        /// Find user and their roles by id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/User/userId
        ///     {
        ///        "userId": 1
        ///     }
        /// </remarks>
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> FindUserById(int userId)
        {
            try
            {
                _logger.LogInformation("Find user by id: userId - {userId}", userId);

                var user = await _userService.GetUserById(userId);
                if (user != null)
                {
                    _logger.LogInformation("Found user by id");

                    return Ok(user);
                }
                _logger.LogInformation("User not found");

                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal error 500: {ex.Message}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="newUser">New user</param>
        /// <returns>New user</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/User/newUser
        ///     {
        ///        "id": 0, (id is always 0)
        ///        "name": "Anna",
        ///        "age": 18,
        ///        "email" : "example@mail.ru"
        ///     }
        ///     id will be incremented
        /// </remarks>
        [HttpPost]
        public async Task<ActionResult<User>> AddNewUser(User newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Add new user");
                    var user = await _userService.AddUser(newUser);

                    if (user != null)
                    {
                        _logger.LogInformation("User was added");

                        return Ok(user);
                    }
                }
                _logger.LogInformation("The fields are entered incorrectly");

                return BadRequest("The fields are entered incorrectly");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal error 500: {ex.Message}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new user record");
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete/User/id
        ///     {
        ///        "id": 1
        ///     }
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserById(int id)
        {
            try
            {
                _logger.LogInformation("Delete user: id - {id}", id);
                if (await _userService.DeleteUser(id))
                {
                    _logger.LogInformation("User deleted");

                    return Ok("User deleted");
                }
                _logger.LogInformation("User not found");

                return BadRequest("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal error 500: {ex.Message}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        /// <summary>
        /// Update user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="user">New user</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put/User/userId/user
        ///     {
        ///        "userId": 1, 
        ///        "user":
        ///        {
        ///          "id": 1,  (id must be equals userId)
        ///          "name": "Anna",
        ///          "age" : 18,
        ///          "Email" : "example@mail.ru"
        ///     }
        /// </remarks>
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUserById(int id, User user)
        {
            try
            {
                _logger.LogInformation("Update user by id: id - {id}", id);
                if (id == user.Id)
                {
                    var updatedUser = await _userService.UpdateUser(id, user);

                    if (updatedUser != null)
                    {
                        _logger.LogInformation("User has updated");

                        return Ok(true);
                    }
                }
                _logger.LogInformation("Id are not equal");

                return NotFound("Id are not equal");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal error 500: {ex.Message}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        /// <summary>
        /// Getting list all users with pagination, filtering, sorting
        /// </summary>
        /// <param name="nameColumnToFilter">Name of the attribute to filter</param>
        /// <param name="filter">Text to filter</param>
        /// <param name="nameColumnToSort">Name of the attribute to sort</param>
        /// <param name="pageIndex">Page number</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/User/nameColumnToFilter/filter/nameColumnToSort/pageIndex
        ///     {
        ///        "nameColumnToFilter" : "username",
        ///        "filter" : "An",
        ///        "nameColumnToSort" : "username",
        ///        "pageIndex" : 1
        ///     }
        /// </remarks>
        [HttpGet("nameColumnToFilter/filter/{nameColumnToSort}/{pageIndex}")]
        public async Task<ActionResult<List<User>>> UsersList(string? nameColumnToFilter, string? filter, string nameColumnToSort, int pageIndex)
        {
            try
            {
                _logger.LogInformation("Getting list all users with pagination, filtering, sorting :" +
                    " nameColumnToFilter - {nameColumnToFilter}, filter - {filter}," +
                    " nameColumnToSort - {nameColumnToSort}, pageIndex - {pageIndex}",
                    nameColumnToFilter, filter, nameColumnToSort, pageIndex);

                var users = await _userService.GetAllUsers(nameColumnToFilter, filter, pageIndex, nameColumnToSort);

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal error 500: {ex.Message}", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
