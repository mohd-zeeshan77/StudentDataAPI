using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Dtos;
using WebAppStudent.Services;

namespace WebAppStudent.Controllers
{
    [Route("api/teacher")]
    public sealed class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;
        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService ?? throw new ArgumentNullException(nameof(teacherService));
        }
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            IEnumerable<TeacherDto> teachers = _teacherService.GetTeacherts();
            return Ok(teachers);
        }
        [HttpGet]
        [Route("{Id:int}")]
        public IActionResult Get(int Id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            TeacherDto? teacher = _teacherService.GetTeacherById(Id);
            return teacher is null ? Problem("Unable to find Teacher. Check log for more info") : Ok(teacher);
        }
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody]CreateTeacherRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            TeacherDto? teacher = _teacherService.CreateTeacher(request);
            return teacher is null ? Problem("Unable to create Teacher. Check log for more Info") : Ok(teacher);
        }
        [HttpPut]
        [Route("{Id:int}")]
        public IActionResult Update(int Id,[FromBody]CreateTeacherRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            TeacherDto? teacher = _teacherService.UpdateTeacher(Id, request);
            return teacher is null ? Problem("Unable to update Teacher. Check Log for more info") : Ok(teacher);
        }
        [HttpPatch]
        [Route("{Id:int}")]
        public IActionResult Active(int Id ,[FromBody]ActiveRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            TeacherDto? teacher = _teacherService.ChangeActiveTeacher(Id, request);
            return teacher is null ? Problem("Unable to change Active status. Check Log for more info") : Ok(teacher);
        }
    }
}
