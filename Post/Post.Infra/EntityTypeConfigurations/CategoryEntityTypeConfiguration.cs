using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entities.CategoryAggregate;

namespace Post.Infra.EntityTypeConfigurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(new Category(id: 1, title: "تکنولوژی"));
        builder.HasData(new Category(id: 2, title: "فین تک"));
    }
}
