using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Dtos;
using WebAppStudent.Services;

namespace WebAppStudent.Controllers
{
    public class MarksController : ControllerBase
    {
        private readonly MarksService _marksService;
        public MarksController(MarksService marksService)
        {
            _marksService = marksService ?? throw new ArgumentNullException(nameof(marksService));
        }
        [HttpGet]
        [Route("api/marks")]
        public IActionResult Get()
        {
            IEnumerable<MarksDto> marks = _marksService.GetMarks();
            return marks is null ? Problem("Unable to find marks. Check log for more info") : Ok(marks);
        }
        [HttpGet]
        [Route("api/student/{StudentId:int}/marks")]
        public IActionResult GetByStudent(int StudentId)
        {
            IEnumerable<MarksDto>? marks =  _marksService.GetMarkByStudent(StudentId);
            return marks is null ? Problem("Unable to find marks by student. Check log for more info") : Ok(marks);
        }
        [HttpGet]
        [Route("api/subject/{SubjectId:int}/marks")]
        public IActionResult GetBySubject(int SubjectId)
        {
            IEnumerable<MarksDto>? marks = _marksService.GetMarksBySubject(SubjectId);
            return marks is null? Problem("Unable to find marks by subject. Check log for more info") : Ok(marks);
        }
        [HttpGet]
        [Route("api/student/{StudentId:int}/subject/{SubjectId:int}/marks")]
        public IActionResult GetMarksByStudentSubject(int StudentId, int SubjectId)
        {
            IEnumerable<MarksDto>? marks = _marksService.GetMarksByStudentSubject(StudentId, SubjectId);
            return marks is null? Problem("Unable to find marks by subject and student. Check log for more info") : Ok(marks);
        }

        [HttpPost]
        [Route("api/student/{StudentId:int}/subject/{SubjectId:int}/marks")]
        public IActionResult AddMarks(int StudentId,int SubjectId,[FromBody]CreateMarksRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            MarksDto? marks = _marksService.CreateMarks(StudentId, SubjectId,request);
            return marks is null? Problem("Unable to add marks. Check log for more info") : Ok(marks);
        }
        [HttpPut]
        [Route("api/student/{StudentId:int}/subject/{SubjectId:int}/marks")]
        public IActionResult UpdateMarks(int StudentId,int SubjectId, [FromBody] CreateMarksRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            MarksDto? marks = _marksService.UpdateMarks(StudentId,SubjectId,request);
            return marks is null? Problem("Unable to update marks. Check log for more info") : Ok(marks);
        }
    }
}
