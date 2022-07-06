using client.core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace client.infrastructure.Data.Configuration
{
    public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persona");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("nombre");
            builder.Property(x => x.Gender).HasColumnName("genero");
            builder.Property(x => x.Age).HasColumnName("edad");
            builder.Property(x => x.Identification).HasColumnName("identificacion");
            builder.Property(x => x.Address).HasColumnName("direccion");
            builder.Property(x => x.Phone).HasColumnName("telefono");
            
            
            
            builder.HasOne(p => p.Client)
                .WithOne(c => c.Person)
                .HasForeignKey<Client>(p => p.PersonId);
        }
    }
}