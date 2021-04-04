namespace UniversityDatabaseImplement.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        public int GradebookNumber { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Student Student { get; set; }
    }
}
