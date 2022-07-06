using client.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace client.infrastructure.Data.Configuration
{
    public class TransactionEntityConfiguration: IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("movimiento");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).HasColumnName("fecha");
            builder.Property(x => x.Type).HasColumnName("tipo_movimiento");
            builder.Property(x => x.Amount).HasColumnName("valor");
            builder.Property(x => x.Balance).HasColumnName("saldo");
        }
    }
}