using SolidCP.WebPortal.Blazor.Components;

namespace SolidCP.WebPortal.Blazor.Data.Services
{
    public class NotificationService
    {
        public event EventHandler<NotificationEventArgs>? NotificationRequested;

        public Task ShowSuccess(string message, string? title = null)
        {
            NotificationRequested?.Invoke(this, new NotificationEventArgs
            {
                Type = UiNotification.NotificationType.Success,
                Title = title ?? "Success",
                Message = message
            });
        }

        public Task ShowError(string message, string? title = null)
        {
            NotificationRequested?.Invoke(this, new NotificationEventArgs
            {
                Type = UiNotification.NotificationType.Error,
                Title = title ?? "Error",
                Message = message
            });
        }

        public  Task ShowWarning(string message, string? title = null)
        {
            NotificationRequested?.Invoke(this, new NotificationEventArgs
            {
                Type = UiNotification.NotificationType.Warning,
                Title = title ?? "Warning",
                Message = message
            });
        }

        public  Task ShowInfo(string message, string? title = null)
        {
            NotificationRequested?.Invoke(this, new NotificationEventArgs
            {
                Type = UiNotification.NotificationType.Info,
                Title = title ?? "Information",
                Message = message
            });
        }
    }

    public class NotificationEventArgs : EventArgs
    {
        public UiNotification.NotificationType Type { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}