using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } 
        public string Password { get; set; }
        public string Email { get; set; }
        public long BranchId { get; set; }
        public long RoleId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }

        #region Foreign Key Relation
        public virtual Branch Branch { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }

    public class UserConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.ToTable("Users", "dbo");

            builder.Property(t => t.FullName).HasMaxLength(200);
            builder.Property(t => t.Password).HasMaxLength(2000);
            builder.Property(t => t.Email).HasMaxLength(200);
        }
    }
}
