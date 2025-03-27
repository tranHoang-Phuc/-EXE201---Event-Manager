using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagerAPI.Models
{
	public class EventSetDbContext : IdentityDbContext<AppUser, Role, string>
	{
		public EventSetDbContext()
		{
		}

		public EventSetDbContext(DbContextOptions<EventSetDbContext> options)
		: base(options)
		{
		}

		public DbSet<Event> Events { get; set; }
		public DbSet<Agenda> Agendas { get; set; }
		public DbSet<EventCategory> EventCategory { get; set; }
		public DbSet<AppUser> Users { get; set; }

		public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<InvalidatedToken> InvalidatedTokens { get; set; }

        public DbSet<Supplier> Suppliers { get; set; } 
        public DbSet<SupplierCategory> SupplierCategories { get; set; }

        public DbSet<TaskJob> TaskJobs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
			string connectStr = config.GetConnectionString("DemoConnectStr");
			optionsBuilder.UseSqlServer(connectStr);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			base.OnModelCreating(modelBuilder);
            //SeedData.ApplySeedData(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				var tableName = entityType.GetTableName();
				if (tableName.StartsWith("AspNet"))
				{
					entityType.SetTableName(tableName.Substring(6));
				}
			}
			
			modelBuilder.Entity<Event>()
				.HasOne(e => e.EventCategory)
				.WithMany(c => c.Events)
				.HasForeignKey(e => e.CategoryId);

			modelBuilder.Entity<Event>()
				.HasOne(e => e.Organizer)
				.WithMany()
				.HasForeignKey(e => e.OrganizerId);

			modelBuilder.Entity<Agenda>()
				.HasOne(a => a.Event)
				.WithMany(e => e.Agendas)
				.HasForeignKey(a => a.EventId);
		

			modelBuilder.Entity<EventParticipant>()
				.HasOne(ep => ep.Event)
				.WithMany(e => e.EventParticipants)
				.HasForeignKey(ep => ep.EventId)
				.OnDelete(DeleteBehavior.NoAction); 

			modelBuilder.Entity<EventParticipant>()
				.HasOne(ep => ep.User)
				.WithMany(u => u.EventParticipants)
				.HasForeignKey(ep => ep.UserId)
				.OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<InvalidatedToken>(entity =>
            {
                entity.ToTable("InvalidatedTokens"); 
                entity.HasKey(it => it.Id); 
                entity.HasOne<AppUser>()
                    .WithMany()
                    .HasForeignKey(it => it.UserId)
                    .OnDelete(DeleteBehavior.NoAction); 
            });

            modelBuilder.Entity<AppUser>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).HasMaxLength(450); 
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.Property(r => r.Id).HasMaxLength(450); 
            });

            modelBuilder.Entity<Supplier>()
            .HasOne(s => s.SupplierCategory)
            .WithMany(c => c.Suppliers)
            .HasForeignKey(s => s.SupplierCategoryId)
            .OnDelete(DeleteBehavior.Cascade);


            // Thiết lập mối quan hệ giữa TaskJob và Event
            modelBuilder.Entity<TaskJob>()
                .HasOne(tj => tj.Event)
                .WithMany(e => e.TaskJobs) // 1 Event có nhiều TaskJob
                .HasForeignKey(tj => tj.EventId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa TaskJob nếu Event bị xóa

        }
	}
}
