using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppStudent.Data;

namespace WebAppStudent.Dtos
{
    public class MarksDto(int Id,int StudentId,int SubjectId,decimal MarksObtained)
    {
        [Key]
        public int Id { get; } = Id;
        [Required]
        [ForeignKey(nameof(Student))]
        public int StudentId { get; } = StudentId;
        [Required]
        [ForeignKey(nameof(Subjects))]
        public int SubjectId { get; } = SubjectId;
        [Required]
        public decimal MarksObtained { get; } = MarksObtained;
    }
}
