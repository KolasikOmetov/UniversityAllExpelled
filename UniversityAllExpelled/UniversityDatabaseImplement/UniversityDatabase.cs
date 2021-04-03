using UniversityDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace  UniversityDatabaseImplement
{
    class UniversityDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog= UniversityAllExpelledWarehouserViewDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Student> Students { set; get; }
        public virtual DbSet<Subject> Subject { set; get; }
        public virtual DbSet<Lector> Lectors { set; get; }
        public virtual DbSet<StudentSubject> StudentSubjects { set; get; }
        public virtual DbSet<CheckList> CheckLists { set; get; }
        public virtual DbSet<Department> Department { set; get; }
    }
}
