using Microsoft.EntityFrameworkCore;
using NASAProj.Domain.Entities.Quizzes;
using NASAProj.Domain.Models;

namespace NASAProj.Data.DbContexts
{
    public class NASAProjDbContext : DbContext
    {
        public NASAProjDbContext(DbContextOptions<NASAProjDbContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Documentation> Documentations { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
