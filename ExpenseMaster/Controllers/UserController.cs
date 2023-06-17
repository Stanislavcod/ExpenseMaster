﻿using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersByLogin(string login)
        {
            var users = await _userService.GetUsersByLoginAsync(login);

            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteUser(int id)
        {
            var existingUser = await _userService.GetUserByIdAsync(id);
            await _userService.DeleteUserAsync(existingUser);

            return Ok(existingUser.Id);
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await _userService.UpdateUserAsync(userUpdateDto);

            return Ok(user);
        }
    }
}
