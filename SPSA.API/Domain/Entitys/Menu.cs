using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long? MenuOrder { get; set; }
        public int MenuLevel { get; set; }
        public bool IsLeaf { get; set; }
        public string? Action { get; set; }

        #region Foreign Key Relation
        public virtual List<RoleWiseMenu> RoleWiseMenus { get; set; }
        #endregion
    }

    public class MenuConfiguration : BaseEntityTypeConfiguration<Menu>
    {
        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            base.Configure(builder);
            builder.ToTable("Menus", "dbo");

            builder.Property(b => b.Name).HasMaxLength(100);
            builder.Property(b => b.Action).HasMaxLength(100);
        }
    }
}
