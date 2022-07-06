using client.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace client.infrastructure.Data.Configuration
{
    public class ClientEntityConfiguration: IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("cliente");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Password).HasColumnName("password");
            builder.Property(x => x.Status).HasColumnName("estado");
        }
    }
}