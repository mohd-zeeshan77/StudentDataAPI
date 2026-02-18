using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppStudent.Data;

namespace WebAppStudent.Dtos
{
    public sealed class SubjectDto(int Id,string Name,int TeacherId)
    {
        [Key]
        public int Id { get; } = Id;
        [Required]
        public string Name { get; } = Name;
        [Required]
        [ForeignKey(nameof(Teachers))]
        public int TeacherId { get; } = TeacherId;
    }
}
