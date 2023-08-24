namespace SPSA.API.Helper.SignalR
{
    public interface IPushNotificationHub
    {
        Task SendNotification(PushNotificationHubDto dto);
        Task SendNotificationToRole(long roleId, PushNotificationHubDto dto);
        Task SendNotificationToUser(long userId, PushNotificationHubDto dto);
    }
}
