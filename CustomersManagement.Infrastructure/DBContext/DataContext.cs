using CustomersManagement.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomersManagement.Infrastructure.DBContext;


public partial class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Customer> Customers { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        OnModelCreatingPartial(builder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
