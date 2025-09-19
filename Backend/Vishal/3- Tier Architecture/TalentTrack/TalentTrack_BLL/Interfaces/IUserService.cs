using TalentTrack_BLL.Dtos;

namespace TalentTrack_BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<string> AddUserAsync(UserCreateUpdateDto userDto);
        Task<string> UpdateUserAsync(int id, UserCreateUpdateDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
