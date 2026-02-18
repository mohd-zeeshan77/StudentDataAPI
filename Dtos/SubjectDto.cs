namespace WebAppStudent.Dtos
{
    public sealed class SubjectDto(int Id,string Name,int TeacherId)
    {
        public int Id { get; } = Id;
        public string Name { get; } = Name;
        public int TeacherId { get; } = TeacherId;
    }
}
