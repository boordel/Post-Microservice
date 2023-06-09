﻿using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities.CategoryAggregate;
using Post.Domain.Entities.PostAggregate;
using Post.Infra.EntityTypeConfigurations;

namespace Post.Infra;

public class PostDBContext: DbContext
{
    DbSet<Domain.Entities.PostAggregate.Post> Posts { get; set; }
    DbSet<PostMedia> PostMedias { get; set; }
    DbSet<Category> Categories { get; set; }

    public PostDBContext(DbContextOptions<PostDBContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostMediaEntityTypeConfiguration());
    }
}
