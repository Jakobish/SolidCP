namespace SolidCP.WebPortal.Blazor.Services
{
    /// <summary>
    /// Feature flags for controlled rollout of Blazor UI components
    /// </summary>
    public class FeatureFlags
    {
        private readonly IConfiguration _configuration;

        public FeatureFlags(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Enable the new Blazor UI for specific user segments
        /// </summary>
        public bool UseBlazorUI => GetFlag("Features.UseBlazorUI", false);

        /// <summary>
        /// Enable Blazor UI for all users (staging/qa)
        /// </summary>
        public bool EnableBlazorUIForAll => GetFlag("Features.EnableBlazorUIForAll", false);

        /// <summary>
        /// Enable specific migrated pages
        /// </summary>
        public bool EnableUserAccountDetails => GetFlag("Pages.EnableUserAccountDetails", true);
        
        /// <summary>
        /// Enable advanced UI features (charts, animations, etc.)
        /// </summary>
        public bool EnableAdvancedUI => GetFlag("Features.EnableAdvancedUI", false);

        /// <summary>
        /// Enable dark mode theme
        /// </summary>
        public bool EnableDarkMode => GetFlag("Features.EnableDarkMode", true);

        /// <summary>
        /// Enable responsive mobile design
        /// </summary>
        public bool EnableResponsiveDesign => GetFlag("Features.EnableResponsiveDesign", true);

        /// <summary>
        /// Enable the new notification system
        /// </summary>
        public bool EnableNotifications => GetFlag("Features.EnableNotifications", true);

        /// <summary>
        /// Check if Blazor UI should be used for a specific user
        /// </summary>
        public bool ShouldUseBlazorUI(int userId)
        {
            if (EnableBlazorUIForAll)
                return true;

            if (!UseBlazorUI)
                return false;

            // Gradual rollout: enable for specific user IDs or email domains
            var enabledUsers = GetFlagArray("Features.BlazorUIEnabledUsers");
            var enabledDomains = GetFlagArray("Features.BlazorUIEnabledDomains");

            // Check if user ID is explicitly enabled
            if (enabledUsers.Contains(userId.ToString()))
                return true;

            // Check if user's email domain is enabled (would need user service integration)
            // This is a placeholder for domain-based rollout
            return false;
        }

        /// <summary>
        /// Get feature flag value with default
        /// </summary>
        public bool GetFlag(string key, bool defaultValue)
        {
            var value = _configuration[key];
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            return bool.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Get comma-separated list as array
        /// </summary>
        private List<string> GetFlagArray(string key)
        {
            var value = _configuration[key];
            if (string.IsNullOrEmpty(value))
                return new List<string>();

            return value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                       .ToList();
        }
    }

    /// <summary>
    /// Extension methods for easier feature flag usage
    /// </summary>
    public static class FeatureFlagExtensions
    {
        /// <summary>
        /// Check if a specific page should use Blazor UI
        /// </summary>
        public static bool ShouldUseBlazorPage(this FeatureFlags flags, string pageName)
        {
            return pageName switch
            {
                "UserAccountDetails" => flags.EnableUserAccountDetails && flags.UseBlazorUI,
                _ => flags.UseBlazorUI
            };
        }

        /// <summary>
        /// Get rollout percentage for gradual feature deployment
        /// </summary>
        public static int GetRolloutPercentage(this FeatureFlags flags, string featureKey)
        {
            return flags.GetFlag($"{featureKey}.RolloutPercentage", 0);
        }

        /// <summary>
        /// Check if feature should be enabled based on rollout percentage
        /// </summary>
        public static bool ShouldRollout(this FeatureFlags flags, string featureKey, int userId)
        {
            var percentage = flags.GetRolloutPercentage(featureKey);
            if (percentage >= 100)
                return true;
            if (percentage <= 0)
                return false;

            // Simple hash-based distribution
            var hash = userId.GetHashCode();
            return (Math.Abs(hash) % 100) < percentage;
        }
    }
}