using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CustomCakeApi.Models
{
    public class CustomCakeContext : DbContext
    {
        protected readonly IConfiguration configuration;
        public CustomCakeContext(DbContextOptions<CustomCakeContext> options, IConfiguration configuration)
            :base(options)
        {
           this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        // connect to sql server with connection string from app settings
        optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("CustomCakeConnection"));
        }
        public DbSet<Cake> Cakes { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
    }
}