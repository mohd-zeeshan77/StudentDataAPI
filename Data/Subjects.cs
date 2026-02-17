using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppStudent.Data
{
    public sealed class Subjects
    {
        [Key]
        public required int Id { get; set; }
        public required string Name { get; set; }
        [ForeignKey("Teachers")]
        public required int TeacherId {  get; set; }
    }
}
