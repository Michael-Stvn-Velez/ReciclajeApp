using Microsoft.EntityFrameworkCore;

namespace ReciclajeApp.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){
        
    }
}