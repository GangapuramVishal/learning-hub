using ChatHistoryDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatHistoryDemo
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<Conversation>()
                .HasKey(c => c.ConversationID);

            modelBuilder.Entity<ChatMessage>()
                .HasKey(m => m.ChatMessageID);

            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithOne(m => m.Conversation)
                .HasForeignKey(m => m.ConversationID)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete if Conversation is deleted

            base.OnModelCreating(modelBuilder);
        }
    }

}
