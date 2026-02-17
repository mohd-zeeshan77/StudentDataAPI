using System.ComponentModel.DataAnnotations;

namespace WebAppStudent.Dtos
{
    public sealed class TeacherDto(int Id, string Name,bool IsActive)
    {
        [Key]
        public int Id { get; } = Id;
        [Required]
        public string Name { get; } = Name;
        [Required]
        public bool IsActive { get; } = IsActive;
    }
}
