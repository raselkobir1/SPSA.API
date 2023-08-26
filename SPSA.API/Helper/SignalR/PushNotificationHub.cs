using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SPSA.API.Helper.SignalR
{
    [Authorize]
    public class PushNotificationHub : Hub, IPushNotificationHub
    {
        private readonly IHubContext<PushNotificationHub> _context;
        private static readonly Dictionary<long, string> userConnectionMap = new();
        public PushNotificationHub(IHubContext<PushNotificationHub> context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            // Associate the user ID with the connection ID when a user connects
            var userId = Convert.ToInt64(Context.UserIdentifier);
            var connectionId = Context.ConnectionId;

            userConnectionMap[userId] = connectionId;
            await Groups.AddToGroupAsync(Context.ConnectionId, "RoleId");

            await Clients.All.SendAsync("PushNotification",$"{Context.ConnectionId} has joined");

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Remove the user ID from the connection map when a user disconnects 
            var userId = Convert.ToInt64(Context.UserIdentifier);

            if (userConnectionMap.ContainsKey(userId))
            {
                userConnectionMap.Remove(userId);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "RoleId");
            await Clients.All.SendAsync("PushNotification", $"{Context.ConnectionId} has disconnect");
            await base.OnDisconnectedAsync(exception);    
        }


        public async Task SendNotification(PushNotificationHubDto dto)
        {
            await _context.Clients.All.SendAsync("PushNotification", dto);
        }

        public async Task SendNotificationToRole(long roleId, PushNotificationHubDto dto)
        {
            await Clients.Group(roleId.ToString()).SendAsync("PushNotification", dto);
        }

        public async Task SendNotificationToUser(long userId, PushNotificationHubDto dto)
        {
            if(userConnectionMap.TryGetValue(userId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("PushNotification",dto);
            }
        }
    }
}
