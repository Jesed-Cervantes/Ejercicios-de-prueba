using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        
        public DbSet<TodoUser> TodoUsers { get; set; }
        public DbSet<TodoMusica> todoMusicas { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //    modelBuilder.Entity<TodoMusica>()
        //        .HasOne(m => m.User)
        //        .WithMany(u => u.Musicas)
        //        .HasForeignKey(m => m.UserId)
        //        .OnDelete(DeleteBehavior.Cascade); 
        //}
    }
}
