using Academy_2025.Data;
using Academy_2025.Dtos;
using Academy_2025.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Academy_2025.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email!);

            if (user is null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                loginDto.Password!);

            return result == PasswordVerificationResult.Failed
                ? null
                : user;
        }
        public async Task<User?> GetCurrentUserAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}