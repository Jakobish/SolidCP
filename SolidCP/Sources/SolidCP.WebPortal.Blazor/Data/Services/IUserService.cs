using SolidCP.WebPortal.Blazor.Data.DTOs;

namespace SolidCP.WebPortal.Blazor.Data.Services
{
    public interface IUserService
    {
        Task<UserDetailsDto?> GetUserDetailsAsync(int userId);
        Task<UserDetailsDto?> GetCurrentUserAsync();
        Task<bool> UpdateUserAsync(UserDetailsDto user);
        Task<bool> UpdateUserMfaAsync(int userId, bool enabled);
        Task<bool> CanUserChangeMfaAsync(int userId);
        Task<List<UserRoleType>> GetAvailableRolesAsync(int userId, UserRoleType currentRole);
    }
}