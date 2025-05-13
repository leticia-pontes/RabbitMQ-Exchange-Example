using Microsoft.EntityFrameworkCore;
using DespachoApi.Models;

namespace DespachoApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Despacho> Despachos { get; set; }
}