using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPSA.API.Helper.CommonMethods;

namespace SPSA.API.Domain.Entitys
{
    public class PushNotification
    {
        public long Id { get; set; }
        public string BookingId { get; set; }
        public long Status { get; set; }
        public string Message { get; set; }
        public DateTime SendTime { get; set; } = CommonMethods.GetCurrentTime();
        public DateTime? ReadTime { get; set; }
        public long? ReadBy { get; set; }


        #region Foreign Key Relation
        public virtual User ReadByUser { get; set; }
        #endregion
    }
    public class PushNotificationConfiguration : IEntityTypeConfiguration<PushNotification>
    {
        public void Configure(EntityTypeBuilder<PushNotification> builder)
        {
            builder.ToTable("PushNotifications", "dbo");

            builder.HasKey(t => t.Id);

            builder.Property(e => e.BookingId).HasMaxLength(20);
            builder.Property(e => e.Message).HasMaxLength(50);
            builder.Property(t => t.SendTime).HasDefaultValueSql("GETDATE()");

            builder.HasOne(t => t.ReadByUser)
              .WithMany()
              .HasForeignKey(t => t.ReadBy);
        }
    }
}
