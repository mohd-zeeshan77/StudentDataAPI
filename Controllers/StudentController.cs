using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Data;
using WebAppStudent.Dtos;
using WebAppStudent.Services;

namespace WebAppStudent.Controllers
{
    [Route("api/student")]
    public sealed class StudentController : ControllerBase
    {
        private StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            IEnumerable<StudentDto> students = _studentService.GetStudents();

            return Ok(students);
        }
        [HttpGet]
        [Route("{Id:int}")]
        public IActionResult Get(int Id)
        {
            StudentDto? student = _studentService.GetStudent(Id);
            return student is null ? NotFound() : Ok(student);
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] CreateStudentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            StudentDto? student = _studentService.CreateStudent(request);
            return student is null ? Problem("There was some problem. See log for more details.") : Ok(student);
        }
        [HttpPut]
        [Route("{Id:int}")]
        public IActionResult Update(int Id, [FromBody] CreateStudentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            StudentDto? updated = _studentService.UpdateStudent(Id, request);
            return updated is null ? Problem("There was problem while updating .See logs for more detials.") : Ok(updated);
        }
        [HttpPatch]
        [Route("{Id:int}")]
        public IActionResult ActiveStatus(int Id, [FromBody] ActiveRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            StudentDto? active = _studentService.Active(Id, request);
            return active is null ? Problem("There is problm in changing active status.See logs for more info") : Ok(active);
        }
    }
}
