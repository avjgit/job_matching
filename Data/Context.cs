using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Leome.Model;

namespace Leome.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PersonTag> PersonTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<Tag>().ToTable("Tags");
            modelBuilder.Entity<PersonTag>().ToTable("PersonTags");
        }
    }
}
