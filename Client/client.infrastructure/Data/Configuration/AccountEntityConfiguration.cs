using client.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace client.infrastructure.Data.Configuration
{
    public class AccountEntityConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("cuenta");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number).HasColumnName("numero_cuenta");
            builder.Property(x => x.Type).HasColumnName("tipo_cuenta");
            builder.Property(x => x.Balance).HasColumnName("saldo_inicial");
            builder.Property(x => x.IsActive).HasColumnName("estado");
            builder.Property(x => x.ClientId).HasColumnName("id_cliente");
        }
    }
}