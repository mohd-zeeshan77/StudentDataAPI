namespace WebAppStudent.Dtos
{
    public class CreateMarksRequest(decimal MarksObtained)
    {
        public decimal MarksObtained { get; } = MarksObtained;
    }
}
