using AutoMapper;
using ECommerce.Application.Common.Utilities.Exceptions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;   
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        
        public async Task<UserDto> UpdateAsync(string id, UserUpdateRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new NotFoundException($"User with id: {id} not found");
            }

            _mapper.Map(request, user);
            await _userManager.UpdateAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if (user == null)
            {
                throw new NotFoundException($"User with id: {id} not found.");
            }

            await _userManager.DeleteAsync(user);
        }
    }
}