using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Domain.Entities.PostAggregate;

namespace Post.Infra.EntityTypeConfigurations;

public class PostMediaEntityTypeConfiguration : IEntityTypeConfiguration<PostMedia>
{
    public void Configure(EntityTypeBuilder<PostMedia> builder)
    {
        
    }
}
