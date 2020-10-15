using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TelegramBot.ContextModels;

namespace TelegramBot.Context
{
    public partial class ProsisDTCContext : DbContext
    {
        public ProsisDTCContext()
        {
        }

        public ProsisDTCContext(DbContextOptions<ProsisDTCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BotKeys> BotKeys { get; set; }
        public virtual DbSet<ChatsCatalog> ChatsCatalog { get; set; }
        public virtual DbSet<ChatsUsers> ChatsUsers { get; set; }
        public virtual DbSet<Dtcusers> Dtcusers { get; set; }
        public virtual DbSet<RollsCatalog> RollsCatalog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ProsisDTC;User=sa;Password=pass;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BotKeys>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK__BotKeys__DFD83CAE56253461");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasMaxLength(50);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.InUse).HasDefaultValueSql("((0))");

                entity.Property(e => e.UseDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ChatsCatalog>(entity =>
            {
                entity.HasKey(e => e.ChatId)
                    .HasName("PK__ChatsCat__A9FBE7C658D580B8");

                entity.Property(e => e.Chat)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Description).HasMaxLength(100);
            });

            modelBuilder.Entity<ChatsUsers>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DateStamp).HasColumnType("datetime");

                entity.HasOne(d => d.Chat)
                    .WithMany()
                    .HasForeignKey(d => d.ChatId)
                    .HasConstraintName("fk_ChatsUsers_ChatsCatalog");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_ChatsUsers_DTCUsers");
            });

            modelBuilder.Entity<Dtcusers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__DTCUsers__1788CC4CCD685665");

                entity.ToTable("DTCUsers");

                entity.Property(e => e.LastName1)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName2).HasMaxLength(30);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.StatusUser)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(31);

                entity.HasOne(d => d.Roll)
                    .WithMany(p => p.Dtcusers)
                    .HasForeignKey(d => d.RollId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DTCUsers_RollsCatalog");
            });

            modelBuilder.Entity<RollsCatalog>(entity =>
            {
                entity.HasKey(e => e.RollId)
                    .HasName("PK__RollsCat__7886EE5F79EEC7B8");

                entity.Property(e => e.RollId).ValueGeneratedNever();

                entity.Property(e => e.RollDescription)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
