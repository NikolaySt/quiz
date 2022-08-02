using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizService.Data.Models;

namespace QuizService.Data.Configurations
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
        }
    }
}