using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppStudent.Data
{
    public sealed class Marks
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int StudentId {  get; set; }
        [ForeignKey("Subjects")]
        public int SubjectId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public  decimal MarksObtained {  get; set; }
    }
}
