using Microsoft.EntityFrameworkCore;

namespace TwilioJokeTeller.Models
{
    public class JokeTellerContext : DbContext
    {
        public JokeTellerContext(DbContextOptions<JokeTellerContext> options) : base(options)
        {
        }

        public DbSet<Joke> Jokes {get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Joke>().HasData(
                new Joke {ID=1,Question="Why did the bike go to sleep early?",Answer="Because it was too tired!"},
                new Joke {ID=2,Question="What happens when you slap Dwayne Johnson's butt?",Answer="You hit rock bottom!"},
                new Joke {ID=3,Question="Why don't ant eaters get sick?",Answer="Because they are full of antybodies!"},
                new Joke {ID=4,Question="What do you call a fish wearing a bow tie?",Answer="Sofishticated!"},
                new Joke {ID=5,Question="What did one mushroom say to the other mushroom?",Answer="You look like a funguy!"},
                new Joke {ID=6,Question="How many web developers does it take to screw in a lightbulb?",Answer="No seriously guys. I can't figure out how to get to the bulb. There's nothing to grab. Please send help"},
                new Joke {ID=7,Question="What do you call a cow with no legs?",Answer="Ground beef!"}
            );
        }

        public DbSet<VerifiedSubscriber> VerifiedSubscribers {get; set;} = null!;
        public DbSet<UnverifiedSubscriber> UnverifiedSubscribers {get; set;} = null!;
        public DbSet<Subscriber> Subscribers {get; set;} = null!;

        
    }
}