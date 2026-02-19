namespace WebAppStudent.Dtos
{
    public sealed class StudentMarksDto(int StudentId,string StudentName, string SubjectName,decimal MarksObtained)
    {
        public int StudentId { get; } = StudentId;
        public string StudentName { get; } = StudentName;
        public string SubjectName { get; } = SubjectName;
        public decimal MarksObtained { get; } = MarksObtained;
    }
}
