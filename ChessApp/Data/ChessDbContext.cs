using Microsoft.EntityFrameworkCore;
using ChessApp.Models;

namespace ChessApp.Data;

public class ChessDbContext : DbContext
{
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Competition> Competitions { get; set; } = null!;
    public DbSet<Match> Matches { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=chess.db");
    }
}
