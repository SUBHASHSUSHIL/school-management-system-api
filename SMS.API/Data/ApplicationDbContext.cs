using Microsoft.EntityFrameworkCore;
using SMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<FeeCategory> FeeCategories { get; set; }
        public DbSet<FeeStructure> FeeStructures { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentFee> StudentFees { get; set; }
        public DbSet<StudentHomework> StudentHomeworks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here

            modelBuilder.Entity<Timetable>()
        .HasOne(t => t.Class)
        .WithMany()
        .HasForeignKey(t => t.ClassId)
        .OnDelete(DeleteBehavior.Cascade); // Allow cascade only on 1 FK

            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Section)
                .WithMany()
                .HasForeignKey(t => t.SectionId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Prevent cascade path loop

            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); // Or set to NoAction

            modelBuilder.Entity<Timetable>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade conflict

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Student)
                .WithMany()
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // Keep this if you want cascading when student is deleted

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Class)
                .WithMany()
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade path error

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Section)
                .WithMany()
                .HasForeignKey(a => a.SectionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade path error

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Recorder)
                .WithMany()
                .HasForeignKey(a => a.RecordedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Student)
                .WithMany()
                .HasForeignKey(er => er.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // keep cascade if you want to delete results when student is deleted

            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Exam)
                .WithMany()
                .HasForeignKey(er => er.ExamId)
                .OnDelete(DeleteBehavior.Restrict); // avoid cascade path error

            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Subject)
                .WithMany()
                .HasForeignKey(er => er.SubjectId)
                .OnDelete(DeleteBehavior.Restrict); // avoid cascade path error

            modelBuilder.Entity<ExamResult>()
                .HasOne(er => er.Recorder)
                .WithMany()
                .HasForeignKey(er => er.RecordedBy)
                .OnDelete(DeleteBehavior.Restrict); // Recorder should not cascade

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Class)
                .WithMany()
                .HasForeignKey(h => h.ClassId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade loop

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Section)
                .WithMany()
                .HasForeignKey(h => h.SectionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Subject)
                .WithMany()
                .HasForeignKey(h => h.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Homework>()
                .HasOne(h => h.Teacher)
                .WithMany()
                .HasForeignKey(h => h.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.StudentFee)
                .WithMany()
                .HasForeignKey(p => p.StudentFeeId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction()

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.ReceivedByUser)
                .WithMany()
                .HasForeignKey(p => p.ReceivedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
