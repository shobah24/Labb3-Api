using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using webb_API.Models;

namespace webb_API.Data
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Person> People  { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; } = null!;


        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed-data person
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FirstName = "Shokran", LastName = "Bahram", Age = 21, PhoneNumber = "0734567890" },
                new Person { Id = 2, FirstName = "Jay", LastName = "Loe", Age = 34, PhoneNumber = "0737654321" },
                new Person {Id = 3, FirstName = "John", LastName = "Doe", Age = 25, PhoneNumber = "0731234567" }
                );

            // seed-data interest
            modelBuilder.Entity<Interest>().HasData(
                new Interest { Id = 1, Name = "Programming", Description = "Writing code and building software" },
                new Interest { Id = 2, Name = "Cooking", Description = "Preparing and cooking food" },
                new Interest { Id = 3, Name = "Traveling", Description = "Exploring new places and cultures" }
                );
            // seed-data link
            modelBuilder.Entity<Link>().HasData(
                new Link { Id = 1, Url = "https://www.recept.se", PersonId = 3, InterestId = 2 },
                new Link { Id = 2, Url = "https://github.com", PersonId = 2, InterestId = 1},
                new Link { Id = 3, Url = "https://www.bucketlisttravels.com/round-up/100-bucket-list-destinations", PersonId = 1, InterestId = 3 }
                );
            // seed-data personinterest
            modelBuilder.Entity<PersonInterest>().HasData(
                new PersonInterest { PersonId = 1, InterestId = 3 },
                new PersonInterest { PersonId = 2, InterestId = 1 },
                new PersonInterest { PersonId = 3, InterestId = 2 }
                );

            // many-to-many relation person och personintresse
            modelBuilder.Entity<Person>()
                .HasMany(p => p.PersonInterests)
                .WithOne(pi => pi.Person)
                .HasForeignKey(pi => pi.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // many-to-many relation interest och person (juction table)
            modelBuilder.Entity<PersonInterest>()
                .HasKey(pi => new { pi.PersonId, pi.InterestId });

            // many-to-many relation interest och personintresse
            modelBuilder.Entity<Interest>()
                .HasMany(i => i.PersonInterests)
                .WithOne(pi => pi.Interest)
                .HasForeignKey(pi => pi.InterestId)
                .OnDelete(DeleteBehavior.Restrict);

            // many-to-many relation personintresse och länk
            modelBuilder.Entity<PersonInterest>()
                .HasMany(pi => pi.Links)
                .WithOne(l => l.PersonInterest)
                .HasForeignKey(l => new { l.PersonId, l.InterestId })
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
