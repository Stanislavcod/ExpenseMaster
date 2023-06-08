﻿using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Common.Dto;
using ExpenseMaster.Common.Helpers.Cryptography;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByLoginAsync(userLoginDto.Login);

            if (user == null)
                throw new Exception("Пользователь не найден");

            if (!PasswordHasher.VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Неверный пароль");

            var token = _tokenService.GetToken(user);
            var userDto = new UserDto { Login = user.Login, Email = user.Email, AuthorizationHeader = $"Bearer {token}" };
            return userDto;
        }
    }
}