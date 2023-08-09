using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public long CompanyId { get; set; }
        public string? Email { get; set; }
        public string? Contactnumber { get; set; }
        public string? FacebookUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? YouTubeUrl { get; set; }
        public bool IsActive { get; set; }

        #region Foreign Key Relation
        public virtual Company Company { get; set; }
        public virtual List<User> Users { get; set; }
        #endregion
    }
    public class BranchConfiguration : BaseEntityTypeConfiguration<Branch>
    {
        public override void Configure(EntityTypeBuilder<Branch> builder)
        {
            base.Configure(builder);
            builder.ToTable("Branches", "dbo");


            builder.Property(t => t.Name).HasMaxLength(200);
            builder.Property(t => t.Email).HasMaxLength(200);
            builder.Property(t => t.Contactnumber).HasMaxLength(20);
            builder.Property(t => t.FacebookUrl).HasMaxLength(2000);
            builder.Property(t => t.InstagramUrl).HasMaxLength(2000);
            builder.Property(t => t.YouTubeUrl).HasMaxLength(2000);
        }
    }
}
