using Azure.Core;
using Microsoft.EntityFrameworkCore;
using WebAppStudent.Data;
using WebAppStudent.Dtos;

namespace WebAppStudent.Services
{
    public sealed class TeacherService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<TeacherService> _logger;
        public TeacherService(AppDbContext dbContext, ILogger<TeacherService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }
        public IEnumerable<TeacherDto> GetTeacherts()
        {
            IList<TeacherDto> teachers = _dbContext.Teachers
                                            .Where(s=>s.IsActive)
                                            .Select(s=> new TeacherDto
                                            (
                                               s.Id,
                                               s.Name,
                                               s.IsActive
                                            )).ToArray();
            
            return teachers;
        }
        public TeacherDto? GetTeacherById(int Id)
        {
            try
            {
                Teachers? teacher = _dbContext.Teachers.Find(Id);
                if (teacher is null || !teacher.IsActive)
                {
                    return null;
                }
                return new TeacherDto(teacher.Id, teacher.Name, teacher.IsActive);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while finding teacher with Id. Problem in execution of sql query.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding teacher with Id.");
            }
            return null;
        }
        public TeacherDto? CreateTeacher(CreateTeacherRequest request)
        {
            try
            {
                Teachers? teacher = _dbContext.Teachers.FirstOrDefault(t => t.Name == request.Name);
                if(teacher is not null)
                {
                    return null;
                }
                teacher = new Teachers { Name = request.Name, IsActive = true };
                _dbContext.Add(teacher);
                _dbContext.SaveChanges();
                return new TeacherDto(teacher.Id, teacher.Name, teacher.IsActive);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while creating a teacher with name {Name}. Problem in execution of sql query.",
                    request.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a teacher with name {@teacher}.", request);
            }
            return null;
        }
        public TeacherDto? UpdateTeacher(int Id, CreateTeacherRequest request)
        {
            try
            {
                Teachers? teacher = _dbContext.Teachers.Find(Id);
                if(teacher is null || !teacher.IsActive)
                {
                    return null;
                }
                if(teacher.Name != request.Name)
                {
                    Teachers? teacherName = _dbContext.Teachers.FirstOrDefault(t => t.Name == request.Name);
                    if(teacherName is not null)
                    {
                        return null;
                    }
                }
                teacher.Name = request.Name;
                _dbContext.SaveChanges();
                return new TeacherDto(teacher.Id, teacher.Name, teacher.IsActive);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while updating a teacher with name {Name}. Problem in execution of sql query.",
                    request.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating a teacher with name {@teacher}.", request);
            }
            return null;
        }
        public TeacherDto? ChangeActiveTeacher(int Id,ActiveRequest request)
        {
            try
            {
                Teachers? teacher = _dbContext.Teachers.Find(Id);
                if(teacher== null)
                {
                    return null;
                }
                if (request.IsActive)
                {
                    teacher.IsActive = true;
                }
                else
                {
                    teacher.IsActive = false;
                }
                _dbContext.SaveChanges();
                return new TeacherDto(teacher.Id, teacher.Name, teacher.IsActive);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while changing active status of a teacher with name. Problem in execution of sql query.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while changing active status of a teacher .");
            }
            return null;
        }
    }
}
