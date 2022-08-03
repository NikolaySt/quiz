using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizGame.Service.Data.Models;

namespace QuizGame.Service.Data.Configurations
{
    using static DataConstants.Answer;

    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(MaxTextLength);

            builder
                .Property(c => c.QuestionId)
                .IsRequired();
        }
    }
}