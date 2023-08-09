using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        #region Foreign Key Relation
        public virtual List<User> Users { get; set; }

        public virtual List<RoleWiseMenu> RoleWiseMenus { get; set; }
        #endregion
    }

    public class RoleConfiguration : BaseEntityTypeConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.ToTable("Roles", "dbo");

            builder.Property(b => b.Name).HasMaxLength(50);
        }

    }
}
