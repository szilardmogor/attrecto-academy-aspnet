using Academy_2025.Data;
using Academy_2025.Dtos;
using Academy_2025.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Academy_2025.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public Task CreateAsync(UserDto data)
        {
            var user = new User
            {
                Id = data.Id,
                Email = data.Email!,
                Password = string.Empty,
                Name = data.Name,
                Role = data.Role,
                Age = data.Age
            };

            user.Password = _passwordHasher.HashPassword(
                user,
                data.Password!);

            return _userRepository.CreateAsync(user);
        }


        public Task<bool> DeleteAsync(int id)
            => _userRepository.DeleteAsync(id);

        public async Task<List<UserDto>> GetAdultUsersAsync()
        {
            var users = await _userRepository.GetAdultUsersAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<UserDto?> UpdateAsync(int id, UserDto data)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.Name = data.Name;
                user.Role = data.Role;
                user.Age = data.Age;

                await _userRepository.UpdateAsync();
            }

            return user != null ? MapToDto(user) : null;
        }

        private static UserDto MapToDto(User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Role = user.Role,
            Age = user.Age,
            Email = user.Email,
            Password = user.Password
        };

        private static User MapToModel(UserDto userDto) => new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Role = userDto.Role,
            Age = userDto.Age,
            Email = userDto.Email,
            Password = userDto.Password
        };
    }
}
