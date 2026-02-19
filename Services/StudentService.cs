using Microsoft.EntityFrameworkCore;
using WebAppStudent.Data;
using WebAppStudent.Dtos;

namespace WebAppStudent.Services
{
    public sealed class StudentService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<StudentService> _logger;
        public StudentService(AppDbContext dbContext, ILogger<StudentService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }
        public IEnumerable<StudentDto> GetStudents()
        {
            IList<StudentDto> students = _dbContext.Student
                                            .Where(s=>s.IsActive)
                                            .Select(s => new StudentDto
                                            (
                                                s.Id,
                                                s.Name,
                                                s.Gender,
                                                s.DateOfBirth,
                                                s.Contact,
                                                s.Email,
                                                s.Address,
                                                s.CreatedOn,
                                                s.IsActive
                                            )).ToList();
            return students;
        }
        public StudentDto? GetStudent(int Id)
        {
            Student? student  = _dbContext.Student.Find(Id);
            if (student is null || !student.IsActive) 
            {
                return null;
            }
            return new StudentDto(
                student.Id,
                student.Name,
                student.Gender,
                student.DateOfBirth,
                student.Contact,
                student.Email,
                student.Address,
                student.CreatedOn, 
                student.IsActive);
        }
        public IEnumerable<StudentDto> GetInActiveStudent()
        {
            return _dbContext.Student
                .Where(s => !s.IsActive)
                .Select(s => new StudentDto(s.Id,
                                            s.Name,
                                            s.Gender,
                                            s.DateOfBirth,
                                            s.Contact,
                                            s.Email,
                                            s.Address,
                                            s.CreatedOn,
                                            s.IsActive))
                .ToList(); ;
        }

        public IEnumerable<StudentMarksDto> GetStudentMarks()
        {
            return _dbContext.Marks
                .Join(_dbContext.Student, m => m.StudentId, s => s.Id, (m, s) => new { m, s })
                .Join(_dbContext.Subjects, ms => ms.m.SubjectId, sb => sb.Id, (ms, sb) => new StudentMarksDto(
                    ms.s.Id,
                    ms.s.Name,
                    sb.Name,
                    ms.m.MarksObtained
                    ))
                .ToList();
        }
        public StudentDto? CreateStudent(CreateStudentRequest request)
        {
            try
            {
                Student? student = _dbContext.Student.FirstOrDefault(s => s.Email == request.Email);
                if (student is not null)
                {
                    return null;
                }
                student = new Student
                {
                    Name = request.Name,
                    Gender = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                    Contact = request.Contact,
                    Email = request.Email,
                    Address = request.Address,
                    CreatedOn = DateOnly.FromDateTime(DateTime.Today),
                    IsActive = true
                };
                _dbContext.Add(student);
                _dbContext.SaveChanges();
                return new StudentDto(
                student.Id,
                student.Name,
                student.Gender,
                student.DateOfBirth,
                student.Contact,
                student.Email,
                student.Address,
                student.CreatedOn,
                student.IsActive);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while creating a student with name {Name}. Problem in execution of sql query.",
                    request.Name);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while creating a student with name {@student}.", request);
            }
            return null;
        }
        public StudentDto? UpdateStudent(int Id, CreateStudentRequest request)
        {
            try
            {
                Student? student = _dbContext.Student.Find(Id);
                if (student is null || !student.IsActive)
                {
                    return null;
                }
                if (student.Email != request.Email)
                {
                    Student? StdEmail = _dbContext.Student.FirstOrDefault(s=>s.Email ==request.Email);
                    if (StdEmail != null)
                    {
                        return null;
                    }
                }
                student.Name = request.Name;
                student.Gender = request.Gender;
                student.DateOfBirth = request.DateOfBirth;
                student.Contact = request.Contact;
                student.Email = request.Email;
                student.Address = request.Address;
                _dbContext.SaveChanges();
                return new StudentDto(
                student.Id,
                student.Name,
                student.Gender,
                student.DateOfBirth,
                student.Contact,
                student.Email,
                student.Address,
                student.CreatedOn,
                student.IsActive);
            }
            catch (DbUpdateException ex) 
            {
                _logger.LogError(ex,
                    "Error while updating a student with name {Name}. Problem in execution of sql query.",
                    request.Name);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updatng a state with name {@student}.", request);
            }
            return null;
        }
        public StudentDto? Active(int Id , ActiveRequest request)
        {
            try
            {
                Student? student = _dbContext.Student.Find(Id);
                if (student == null)
                {
                    return null;
                }
                if(request.IsActive)
                {
                    student.IsActive = true;
                }
                else
                {
                    student.IsActive = false;
                }
                _dbContext.SaveChanges();
                return  new StudentDto(
                student.Id,
                student.Name,
                student.Gender,
                student.DateOfBirth,
                student.Contact,
                student.Email,
                student.Address,
                student.CreatedOn,
                student.IsActive);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while changing active status of a student . Problem in execution of sql query.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing active status of a student.");
            }
            return null;
        }
    }
}
