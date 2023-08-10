using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPSA.API.DataAccess.Interfaces;

namespace SPSA.API.Domain
{
    public class BaseEntity: ISoftDeletable
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        #region Foreign Key Relation
        public virtual User CreatedByUser { get; set; } 
        public virtual User UpdatedByUser { get; set; } 
        #endregion
    }

    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> 
    where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.IsDeleted).HasDefaultValueSql("0");
            builder.Property(b => b.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.ModifiedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.CreatedDate).HasColumnType("datetime2");
            builder.Property(b => b.ModifiedDate).HasColumnType("datetime2");

            builder.HasOne(b => b.CreatedByUser)
                .WithMany()
                .HasForeignKey(b => b.CreatedBy)
                .IsRequired();

            builder.HasOne(b => b.UpdatedByUser)
                .WithMany()
                .HasForeignKey(b => b.ModifiedBy)
                .IsRequired();
        }
    }
}
