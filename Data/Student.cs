using System.ComponentModel.DataAnnotations;

namespace WebAppStudent.Data
{
    public sealed class Student
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Gender { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Contact {  get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public DateOnly CreatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
