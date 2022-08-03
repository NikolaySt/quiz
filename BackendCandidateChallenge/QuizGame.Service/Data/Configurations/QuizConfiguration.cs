using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Data.Configurations
{
    using static DataConstants.Quiz;

    internal class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(MaxTextLength);

            builder
                .HasMany(it => it.Questions)
                .WithOne(it => it.Quiz)
                .HasForeignKey(c => c.QuizId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}