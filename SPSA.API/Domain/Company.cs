using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string? LogoUrl { get; set; }

        #region Foreign Key Relation
        public virtual List<Branch> Branches { get; set; }
        #endregion
    }
    public class CompanyConfiguration : BaseEntityTypeConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);
            builder.ToTable("Companies", "dbo");

            builder.Property(t => t.Name).HasMaxLength(200);
            builder.Property(t => t.LogoUrl).HasMaxLength(2000);
        }
    }
}
