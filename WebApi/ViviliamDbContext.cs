using Microsoft.EntityFrameworkCore;

public class ViviliamDbContext : DbContext
{
    public ViviliamDbContext(DbContextOptions<ViviliamDbContext> options) : base(options) { }

    public DbSet<BsiBible> BSI { get; set; }
    public DbSet<KjvBible> KJV { get; set; }
    public DbSet<AmpBible> AMP { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BsiBible>().ToTable("BSI").HasKey(tb => new { tb.Book, tb.Chapter, tb.Verse });
        modelBuilder.Entity<KjvBible>().ToTable("KJV").HasKey(tb => new { tb.Book, tb.Chapter, tb.Verse });
        modelBuilder.Entity<AmpBible>().ToTable("AMP").HasKey(tb => new { tb.Book, tb.Chapter, tb.Verse });
        base.OnModelCreating(modelBuilder);
    }
}
