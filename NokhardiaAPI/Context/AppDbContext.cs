using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NokhardiaAPI.Entities;

namespace NokhardiaAPI.Context
{
	public class AppDbContext:DbContext
	{
		public AppDbContext()
		{
			
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<Character>	 Characters { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var stringListConverter = new ValueConverter<List<string>, string>(
				v => string.Join(";", v),
				v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList()
			);

			modelBuilder.Entity<Character>(entity =>
			{
				entity.Property(e => e.Clans).HasConversion(stringListConverter);
				entity.Property(e => e.Race).HasConversion(stringListConverter);
				entity.Property(e => e.RelatedCharacters).HasConversion(stringListConverter);
				entity.Property(e => e.Skills).HasConversion(stringListConverter);
				entity.Property(e => e.Story).HasConversion(stringListConverter);
			});
		}
	}
}
