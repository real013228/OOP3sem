using DataAccessLayer.Models;
using DataAccessLayer.Models.Employees;
using DataAccessLayer.Models.Levels;
using DataAccessLayer.Models.Messages;
using DataAccessLayer.Models.MessageSources;

namespace DataAccessLayer;

using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<MessageSource> MessageSources { get; set; }
    public DbSet<BaseMessage> Messages { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .ToTable("Employees")
            .HasDiscriminator<int>("Type")
            .HasValue<Worker>(1)
            .HasValue<Manager>(2);
        modelBuilder.Entity<BaseMessage>()
            .ToTable("Messages")
            .HasDiscriminator<int>("Type")
            .HasValue<EmailMessage>(1)
            .HasValue<MobileMessage>(2)
            .HasValue<SmsMessage>(3);
        modelBuilder.Entity<MessageSource>()
            .ToTable("MessageSources")
            .HasDiscriminator<int>("Type")
            .HasValue<EmailMessageSource>(1)
            .HasValue<MobileMessageSource>(2)
            .HasValue<SmsMessageSource>(3);
        modelBuilder.Entity<Session>()
            .ToTable("Sessions");
    }
}