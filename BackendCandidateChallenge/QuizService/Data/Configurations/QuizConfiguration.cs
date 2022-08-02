using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizService.Data.Models;

namespace QuizService.Data.Configurations
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
        }
    }
}