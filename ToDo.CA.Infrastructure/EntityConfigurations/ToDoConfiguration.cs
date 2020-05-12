using Microsoft.EntityFrameworkCore;

namespace ToDo.CA.Infrastructure.EntityConfigurations
{
    public class ToDoConfiguration : IEntityTypeConfiguration<Core.Models.ToDo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Core.Models.ToDo> builder)
        {
            builder.HasKey(_ => _.Id);
        }
    }
}