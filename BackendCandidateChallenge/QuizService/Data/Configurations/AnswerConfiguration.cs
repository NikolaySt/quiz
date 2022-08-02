using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizService.Data.Models;

namespace QuizService.Data.Configurations
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
        }
    }
}