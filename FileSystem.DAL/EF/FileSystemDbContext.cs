using FileSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSystem.DAL.EF {
    public class FileSystemDbContext : IdentityDbContext<User, Role, int> {
        public FileSystemDbContext(DbContextOptions<FileSystemDbContext> options)
            : base(options) { }
        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            base.OnModelCreating(builder);
        }
    }
}
