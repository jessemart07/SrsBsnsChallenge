using Microsoft.EntityFrameworkCore;
using SrsBsnsChallenge.Server.Data.Models;

namespace SrsBsnsChallenge.Server.Data
{
    public class SrsBsnsChallengeDbContext : DbContext
    {
        // We use => (expression-bodied members) to avoid nullable reference types errors.
        // Source: https://docs.microsoft.com/en-us/ef/core/miscellaneous/nullable-reference-types#dbcontext-and-dbset.
        public DbSet<ContactForm> ContactForms => Set<ContactForm>();

        public SrsBsnsChallengeDbContext(DbContextOptions<SrsBsnsChallengeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base version of this method (in DbContext) as well, else we sometimes get an error later on.
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactForm>();
        }
    }
}
