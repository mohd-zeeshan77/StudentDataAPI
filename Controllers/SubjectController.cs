using Microsoft.AspNetCore.Mvc;
using WebAppStudent.Dtos;
using WebAppStudent.Services;

namespace WebAppStudent.Controllers
{
    public class SubjectController : ControllerBase
    {
        private readonly SubjectService _subjectService;
        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService ?? throw new ArgumentNullException(nameof(subjectService)); ;
        }
        [HttpGet]
        [Route("api/subject")]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            IEnumerable<SubjectDto>? subjects  =_subjectService.GetSubjects();

            return subjects is null ? Problem("there is a problem while getting subjects. check log for info") : Ok(subjects);
        }
        [HttpGet]
        [Route("api/subject/{Id:int}")]
        public IActionResult GetByID(int Id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            SubjectDto? subject = _subjectService.GetSubjectById(Id);
            return subject is null ? Problem("Error getting subject by id. Check Log") : Ok(subject);
        }
        [HttpGet]
        [Route("api/teacher/{teacherId:int}/subject")]
        public IActionResult GetByTeacher(int teacherId)
        {
            if(!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            IEnumerable<SubjectDto>? subject =_subjectService.GetSubjectByTeacherID(teacherId);
            return subject is null ? Problem("Error Getting Subject By teacher ID. Check Log"):Ok(subject);
        }
        [HttpPost]
        [Route("api/teacher/{teacherId:int}/subject")]
        public IActionResult CreateSubjet(int teacherId,[FromBody]CreateSubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            SubjectDto? subject = _subjectService.CreateSubject(teacherId, request);
            return subject is null ? Problem("while Creating subject check log"): Ok(subject);
        }
        [HttpPut]
        [Route("api/subject/{Id:int}")]
        public IActionResult Update(int Id, [FromBody] CreateSubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            SubjectDto? sub = _subjectService.UpdateSubject(Id,request);
            return sub is null ? Problem("error occur while updating subject. Check log for more info") : Ok(sub);
        }
    }
}
