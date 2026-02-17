using System.ComponentModel.DataAnnotations;

namespace WebAppStudent.Dtos
{
    public sealed class CreateStudentRequest(string Name,
                                            string Gender,
                                            DateOnly DateOfBirth,
                                            string Contact,
                                            string Email, 
                                            string Address,
                                            DateOnly CreatedOn,
                                            bool IsActive)
    {
        [Required]
        public string Name { get; } = Name;
        [Required]
        public string Gender { get; } = Gender;
        [Required]
        public DateOnly DateOfBirth { get; } = DateOfBirth;
        [Required]
        public string Contact { get; } = Contact;
        [Required]
        public string Email { get; } = Email;
        [Required]
        public string Address { get; } = Address;
        public DateOnly CreatedOn { get; } = CreatedOn;
        public bool IsActive { get; } = IsActive;
    }
}
