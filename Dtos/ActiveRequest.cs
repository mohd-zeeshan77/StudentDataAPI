namespace WebAppStudent.Dtos
{
    public sealed class ActiveRequest(bool IsActive)
    {
        public bool IsActive { get; } = IsActive;
    }
}
