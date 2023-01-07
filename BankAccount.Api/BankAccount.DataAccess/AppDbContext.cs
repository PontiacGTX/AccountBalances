using BankAccount.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.DataAccess
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Account>().HasOne(x => x.User).WithMany(x => x.Accounts);
            base.OnModelCreating(modelBuilder);
        }


    }
}