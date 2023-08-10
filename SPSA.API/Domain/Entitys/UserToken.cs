using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSA.API.Domain
{
    public class UserToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime JWTExpires { get; set; }
        public DateTime RefreshExpires { get; set; }
        public bool IsRevoked { get; set; }

        #region Foreign Key Relation
        public virtual User User { get; set; }
        #endregion
    }

    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens", "dbo");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.JWTToken).HasMaxLength(2000);
            builder.Property(t => t.RefreshToken).HasMaxLength(2000);
            builder.Property(e => e.JWTExpires);
            builder.Property(e => e.RefreshExpires);
        }
    }
}
