using SolidCP.WebPortal.Blazor.Data.DTOs;

namespace SolidCP.WebPortal.Blazor.Data.Services
{
    // Mock implementation for demonstration - replace with actual SolidCP service calls
    public class UserService : IUserService
    {
        private static readonly List<UserDetailsDto> _mockUsers = new()
        {
            new UserDetailsDto
            {
                UserId = 1,
                Username = "admin@solidcp.com",
                FirstName = "John",
                LastName = "Doe",
                SubscriberNumber = "USR001",
                Email = "john.doe@example.com",
                SecondaryEmail = "johndoe@backup.com",
                MailFormat = MailFormatType.HTML,
                Role = UserRoleType.Administrator,
                IsDemo = false,
                MfaEnabled = true,
                LoginStatus = LoginStatusType.Enabled,
                CompanyName = "SolidCP Solutions",
                Address = "123 Main Street",
                City = "New York",
                Country = "USA",
                State = "NY",
                Zip = "10001",
                PrimaryPhone = "+1-555-0123",
                SecondaryPhone = "+1-555-0124",
                Fax = "+1-555-0125",
                MessengerId = "johndoe",
                OriginalStatus = UserStatusType.Active
            }
        };

        public Task<UserDetailsDto?> GetUserDetailsAsync(int userId)
        {
            return Task.FromResult(_mockUsers.FirstOrDefault(u => u.UserId == userId));
        }

        public Task<UserDetailsDto?> GetCurrentUserAsync()
        {
            return Task.FromResult(_mockUsers.FirstOrDefault(u => u.UserId == 1));
        }

        public Task<bool> UpdateUserAsync(UserDetailsDto user)
        {
            // Simulate API call - in real implementation, call SolidCP API
            var existingUser = _mockUsers.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                // Update the user with new data
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.SubscriberNumber = user.SubscriberNumber;
                existingUser.Email = user.Email;
                existingUser.SecondaryEmail = user.SecondaryEmail;
                existingUser.MailFormat = user.MailFormat;
                existingUser.Role = user.Role;
                existingUser.IsDemo = user.IsDemo;
                existingUser.MfaEnabled = user.MfaEnabled;
                existingUser.LoginStatus = user.LoginStatus;
                existingUser.CompanyName = user.CompanyName;
                existingUser.Address = user.Address;
                existingUser.City = user.City;
                existingUser.Country = user.Country;
                existingUser.State = user.State;
                existingUser.Zip = user.Zip;
                existingUser.PrimaryPhone = user.PrimaryPhone;
                existingUser.SecondaryPhone = user.SecondaryPhone;
                existingUser.Fax = user.Fax;
                existingUser.MessengerId = user.MessengerId;
                
                return Task.FromResult(true);
            }
            
            return Task.FromResult(false);
        }

        public Task<bool> UpdateUserMfaAsync(int userId, bool enabled)
        {
            var user = _mockUsers.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.MfaEnabled = enabled;
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> CanUserChangeMfaAsync(int userId)
        {
            // In real implementation, check user permissions
            return Task.FromResult(true);
        }

        public Task<List<UserRoleType>> GetAvailableRolesAsync(int userId, UserRoleType currentRole)
        {
            // Mock role availability - in real implementation, check based on user permissions
            var roles = new List<UserRoleType>();
            
            switch (currentRole)
            {
                case UserRoleType.Administrator:
                    roles.AddRange(new[] { UserRoleType.Administrator, UserRoleType.Reseller, UserRoleType.User });
                    break;
                case UserRoleType.Reseller:
                    roles.AddRange(new[] { UserRoleType.Reseller, UserRoleType.User });
                    break;
                case UserRoleType.User:
                    roles.Add(UserRoleType.User);
                    break;
            }
            
            return Task.FromResult(roles);
        }
    }
}