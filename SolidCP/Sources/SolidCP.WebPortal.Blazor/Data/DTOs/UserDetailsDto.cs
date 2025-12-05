using System.ComponentModel.DataAnnotations;

namespace SolidCP.WebPortal.Blazor.Data.DTOs
{
    public class UserDetailsDto
    {
        public int UserId { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        
        [Display(Name = "Account Number")]
        public string SubscriberNumber { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        
        [EmailAddress]
        [Display(Name = "Secondary Email")]
        public string SecondaryEmail { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Mail Format")]
        public MailFormatType MailFormat { get; set; }
        
        [Required]
        [Display(Name = "Role")]
        public UserRoleType Role { get; set; }
        
        [Display(Name = "Demo Account")]
        public bool IsDemo { get; set; }
        
        [Display(Name = "MFA Enabled")]
        public bool MfaEnabled { get; set; }
        
        [Display(Name = "Login Status")]
        public LoginStatusType LoginStatus { get; set; }
        
        // Contact Information
        [Display(Name = "Company Name")]
        public string? CompanyName { get; set; }
        
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        
        [Display(Name = "Primary Phone")]
        public string? PrimaryPhone { get; set; }
        
        [Display(Name = "Secondary Phone")]
        public string? SecondaryPhone { get; set; }
        
        public string? Fax { get; set; }
        
        [Display(Name = "Messenger ID")]
        public string? MessengerId { get; set; }
        
        // Original status to prevent unauthorized changes
        public UserStatusType OriginalStatus { get; set; }
    }

    public enum MailFormatType
    {
        Text = 0,
        HTML = 1
    }

    public enum UserRoleType
    {
        User = 0,
        Reseller = 1,
        Administrator = 3
    }

    public enum LoginStatusType
    {
        Enabled = 0,
        Disabled = 1,
        LockedOut = 2
    }

    public enum UserStatusType
    {
        Active = 0,
        Suspended = 1,
        Cancelled = 2
    }
}