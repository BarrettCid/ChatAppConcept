using ChatAppContext.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace ChatAppContext.Context
{
    public class ChatAppDBContext : DbContext
    {
        public DbSet<Channel> Channels { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserServer> UserServers { get; set; }
        public DbSet<ServerChannel> ServerChannels { get; set; }

        public ChatAppDBContext()
        {
        }

        public ChatAppDBContext(DbContextOptions<ChatAppDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.ToTable("Channel", "chat");

                entity.Property(e => e.ChannelId).HasColumnName("ChannelId");

                entity.Property(e => e.Name).HasColumnName("Name").IsRequired();

            });

            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.ToTable("LogEntry", "security");

                entity.Property(e => e.LogEntryId).HasColumnName("LogEntryId");

                entity.Property(e => e.UserId).HasColumnName("UserId").IsRequired();

                entity.Property(e => e.Action).HasColumnName("Action").IsRequired();

                entity.Property(e => e.Message).HasColumnName("Message").IsRequired();

                entity.Property(e => e.DateLogged).HasColumnName("DateLogged").IsRequired();

                entity.HasOne(d => d.User)
                .WithMany(p => p.LogEntries)
                .HasConstraintName("FK_LogEntry_User");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message", "chat");

                entity.Property(e => e.MessageId).HasColumnName("MessageId");

                entity.Property(e => e.ChannelId);

                entity.Property(e => e.MessageData).HasColumnName("MessageData").IsRequired();

                entity.HasOne(e => e.Channel)
                .WithMany(e => e.Messages);

                entity.HasOne(e => e.User);

            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token", "security");

                entity.Property(e => e.TokenId).HasColumnName("TokenId");

                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.IssueDate).IsRequired();

            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "chat");

                entity.Property(e => e.ServerId).HasColumnName("ServerId");

                entity.Property(e => e.Name).HasColumnName("Name").IsRequired();

                entity.Property(e => e.Description).HasColumnName("Description");

                entity.Property(e => e.DateCreated).HasColumnName("DateCreated").IsRequired();

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "security");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.EmailAddress).IsRequired().HasMaxLength(50);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.DateCreated)
                    .IsRequired();

                entity.Property(e => e.DateModified);

                entity.HasOne(e => e.Token)
                .WithOne(e => e.User);

            });

            modelBuilder.Entity<UserServer>().HasKey(us => new { us.UserId, us.ServerId });

            modelBuilder.Entity<UserServer>()
                .HasOne(us => us.User)
                .WithMany(s => s.UserServers)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserServer>()
                .HasOne(us => us.Server)
                .WithMany(u => u.UserServers)
                .HasForeignKey(us => us.ServerId);


            modelBuilder.Entity<ServerChannel>().HasKey(sc => new { sc.ServerId, sc.ChannelId });

            modelBuilder.Entity<ServerChannel>()
                .HasOne(sc => sc.Server)
                .WithMany(s => s.ServerChannels)
                .HasForeignKey(sc => sc.ServerId);

            modelBuilder.Entity<ServerChannel>()
                .HasOne(sc => sc.Channel)
                .WithMany(c => c.ServerChannels)
                .HasForeignKey(sc => sc.ChannelId);
        }
    }
}
