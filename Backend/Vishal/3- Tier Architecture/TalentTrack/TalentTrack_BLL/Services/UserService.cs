using TalentTrack_BLL.Dtos;
using TalentTrack_BLL.Interfaces;
using TalentTrack_DAL.Entities;
using TalentTrack_DAL.Interfaces;

namespace TalentTrack_BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserDto
            {
                UserID = u.UserID,
                EmployeeID = u.EmployeeID,
                Name = u.Name,
                Email = u.Email,
                PhoneNo = u.PhoneNo,
                PersonalEmail = u.PersonalEmail,
                Designation = u.Designation,
                ManagerID = u.ManagerID,
                ExperienceYears = u.ExperienceYears,
                AvailabilityStatus = u.AvailabilityStatus
            }).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? null : new UserDto
            {
                UserID = user.UserID,
                EmployeeID = user.EmployeeID,
                Name = user.Name,
                Email = user.Email,
                PhoneNo = user.PhoneNo,
                PersonalEmail = user.PersonalEmail,
                Designation = user.Designation,
                ManagerID = user.ManagerID,
                ExperienceYears = user.ExperienceYears,
                AvailabilityStatus = user.AvailabilityStatus
            };
        }

        public async Task<string> AddUserAsync(UserCreateUpdateDto userDto)
        {
            var user = new User
            {
                EmployeeID = userDto.EmployeeID,
                Name = userDto.Name,
                Email = userDto.Email,
                PhoneNo = userDto.PhoneNo,
                PersonalEmail = userDto.PersonalEmail,
                Designation = userDto.Designation,
                ManagerID = userDto.ManagerID,
                ExperienceYears = userDto.ExperienceYears,
                AvailabilityStatus = userDto.AvailabilityStatus
            };
            await _userRepository.AddUserAsync(user);

            return "User created successfully";
        }

        public async Task<string> UpdateUserAsync(int id, UserCreateUpdateDto userDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return "User not found";

            user.EmployeeID = userDto.EmployeeID;
            user.Name = userDto.Name;
            user.Email = userDto.Email;
            user.PhoneNo = userDto.PhoneNo;
            user.PersonalEmail = userDto.PersonalEmail;
            user.Designation = userDto.Designation;
            user.ManagerID = userDto.ManagerID;
            user.ExperienceYears = userDto.ExperienceYears;
            user.AvailabilityStatus = userDto.AvailabilityStatus;

            await _userRepository.UpdateUserAsync(user);

            return "User updated successfully";
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteUserAsync(id);
            return true;
        }
    }
}
