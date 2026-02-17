using System.ComponentModel.DataAnnotations;

namespace WebAppStudent.Dtos
{
    public class CreateTeacherRequest(string Name , bool IsActive)
    {
        [Required]
        public string Name { get; } = Name;
        [Required]
        public bool IsActive { get; } = IsActive;
    }
}
