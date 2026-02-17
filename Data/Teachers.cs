using System.ComponentModel.DataAnnotations;

namespace WebAppStudent.Data
{
    public sealed class Teachers
    {
        [Key]
        public int Id { get; set; } 
        public required string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
