using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class RoleWiseMenu : BaseEntity
    {
        public long RoleId { get; set; }
        public long MenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }

        #region Foreign Key Relation
        public virtual Role Role { get; set; }
        public virtual Menu Menu { get; set; }
        #endregion
    }
    public class RoleWiseMenuConfiguration : BaseEntityTypeConfiguration<RoleWiseMenu>
    {
        public override void Configure(EntityTypeBuilder<RoleWiseMenu> builder)
        {
            base.Configure(builder);
            builder.ToTable("RoleWiseMenus", "dbo");

            builder.HasIndex(m => new { m.MenuId, m.RoleId }).IsUnique();
        }
    }
}
