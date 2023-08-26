namespace SPSA.API.Helper.SignalR
{
    public class PushNotificationHubDto
    {
        public long Id { get; set; }
        public string BookingId { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
        public string Message { get; set; }
        public DateTime? ReadTime { get; set; }
        public DateTime SendTime { get; set; }
        public long? ReadBy { get; set; }
    }
}
