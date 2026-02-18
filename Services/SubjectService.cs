using Microsoft.EntityFrameworkCore;
using WebAppStudent.Data;
using WebAppStudent.Dtos;

namespace WebAppStudent.Services
{
    public sealed class SubjectService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<SubjectService> _logger;
        public SubjectService(AppDbContext dbContext, ILogger<SubjectService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }
        public IEnumerable<SubjectDto>? GetSubjects()
        {
            return _dbContext.Subjects.Select(s => new SubjectDto(s.Id, s.Name, s.TeacherId)).ToList();
        }
        public SubjectDto? GetSubjectById(int Id)
        {
            try
            {
                Subjects? subject = _dbContext.Subjects.Find(Id);
                if (subject is null)
                {
                    return null;
                }
                return new SubjectDto(subject.Id, subject.Name, subject.TeacherId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding Subject with Id.");
            }
            return null;
        }
        public IEnumerable<SubjectDto>? GetSubjectByTeacherID(int teacherId)
        {
            try
            {
                Teachers? teacher = _dbContext.Teachers.Find(teacherId);
                if (teacher is null || !teacher.IsActive)
                {
                    return null;
                }
                return _dbContext.Subjects
                            .Where(s => s.TeacherId ==teacherId)
                            .Select(s => new SubjectDto(s.Id, s.Name, s.TeacherId))
                            .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while finding Subject with teacher Id.");
            }
            return null;
        }
        public SubjectDto? CreateSubject(int teacherId,CreateSubjectRequest request)
        {
            try
            {
                Teachers? teacher = _dbContext.Teachers.Find(teacherId);
                if(teacher is null || !teacher.IsActive)
                {
                    return null;
                }
                Subjects? subject = _dbContext.Subjects.FirstOrDefault(s => s.Name == request.Name);
                if(subject is not null)
                {
                    return null;
                }
                subject = new Subjects{ Name = request.Name,
                    TeacherId = teacherId
                };
                _dbContext.Add(subject);
                _dbContext.SaveChanges();
                return new SubjectDto(subject.Id, subject.Name, subject.TeacherId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while creating a Subject with name {Name}. Problem in execution of sql query.",
                    request.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating a Subject with name {@subejct}.", request);
            }
            return null;
        }
        public SubjectDto? UpdateSubject(int Id,CreateSubjectRequest request)
        {
            try
            {
                Subjects? subject = _dbContext.Subjects.Find(Id);
                if(subject is null)
                {
                    return null; 
                }
                if(subject.Name != request.Name)
                {
                    Subjects? subjectName = _dbContext.Subjects.FirstOrDefault(s => s.Name == request.Name);
                    if(subjectName is not null)
                    {
                        return null;
                    }
                }
                subject.Name = request.Name;
                _dbContext.SaveChanges();
                return new SubjectDto(subject.Id, subject.Name, subject.TeacherId); 
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while creating a updating with name {Name}. Problem in execution of sql query.",
                    request.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating a Subject with name {@subejct}.", request);
            }
            return null;
        }
    }
}
