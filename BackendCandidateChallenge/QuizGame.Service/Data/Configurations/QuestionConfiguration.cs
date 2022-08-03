using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Data.Configurations
{
    using static DataConstants.Question;

    internal class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Text)
                .IsRequired()
                .HasMaxLength(MaxTextLength);

            builder
                .Property(d => d.QuizId)
                .IsRequired();

            builder
                .HasMany(it => it.Answers)
                .WithOne(it => it.Question)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}