using Microsoft.EntityFrameworkCore;
using WebAppStudent.Data;
using WebAppStudent.Dtos;

namespace WebAppStudent.Services
{
    public class MarksService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<MarksService> _logger;
        public MarksService(AppDbContext dbContext, ILogger<MarksService> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger;
        }
        public IEnumerable<MarksDto> GetMarks()
        {
            return _dbContext.Marks
                        .Select(m=> new MarksDto (m.Id,m.StudentId,m.SubjectId,m.MarksObtained))
                        .ToList();
        }
        public IEnumerable<MarksDto>? GetMarkByStudent(int studentId)
        {
            try
            {
                Student? std = _dbContext.Student.Find(studentId);
                if (std is null)
                {
                    return null;
                }
                return _dbContext.Marks
                                .Where(m=>m.StudentId == studentId)
                                .Select(m => new MarksDto(m.Id, m.StudentId, m.SubjectId, m.MarksObtained))
                                .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "There error while finding marks by Student Id");
            }
            return null;
        }
        public IEnumerable<MarksDto>? GetMarksBySubject(int subjectId)
        {
            try
            {
                Subjects? sub = _dbContext.Subjects.Find(subjectId);
                if(sub is null)
                {
                    return null;
                }
                return _dbContext.Marks
                        .Where(m=>m.SubjectId == subjectId)
                        .Select(m => new MarksDto(m.Id, m.StudentId, m.SubjectId, m.MarksObtained))
                        .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occur While Finding marks with subject Id");
            }
            return null;
        }
        public IEnumerable<MarksDto>? GetMarksByStudentSubject(int StudentId, int SubjectId)
        {
            try
            {
                Student? std = _dbContext.Student.Find(StudentId);
                Subjects? sub = _dbContext.Subjects.Find(SubjectId);
                if (std is null || sub is null)
                {
                    return null;
                }
                if (std.IsActive) return null;
                return _dbContext.Marks
                    .Where(m=>m.SubjectId == SubjectId && m.StudentId ==StudentId)
                    .Select(m => new MarksDto(m.Id, m.StudentId, m.SubjectId, m.MarksObtained))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occur While Finding marks");
            }
            return null;
        }
        public MarksDto? CreateMarks(int StudentId,int SubjectId,CreateMarksRequest request)
        {
            try
            {
                Student? std = _dbContext.Student.Find(StudentId);
                Subjects? sub = _dbContext.Subjects.Find(SubjectId);
                if (std is null || sub is null)
                {
                    return null;
                }
                Marks? marks = _dbContext.Marks.FirstOrDefault(m => m.StudentId == StudentId && m.SubjectId == SubjectId);
                if(marks is not null)
                {
                    return null;
                }
                marks = new Marks
                {
                    StudentId = StudentId,
                    SubjectId = SubjectId,
                    MarksObtained = request.MarksObtained
                };
                _dbContext.Add(marks);
                _dbContext.SaveChanges();
                return new MarksDto(marks.Id, marks.StudentId, marks.SubjectId, marks.MarksObtained);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while Adding marks. Problem in execution of sql query.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding marks");
            }
            return null;
        }
        public MarksDto? UpdateMarks(int StudentId,int SubjectId,CreateMarksRequest request)
        {
            try
            {
                Student? std = _dbContext.Student.Find(StudentId);
                Subjects? sub = _dbContext.Subjects.Find(SubjectId);
                if(std is null || sub is null)
                {
                    return null;
                }
                Marks? marks = _dbContext.Marks.FirstOrDefault(m=>m.StudentId == StudentId && m.SubjectId ==SubjectId);
                if(marks is null)
                {
                    return null;
                }
                marks.MarksObtained = request.MarksObtained;
                _dbContext.SaveChanges();
                return new MarksDto(marks.Id, marks.StudentId, marks.SubjectId, marks.MarksObtained);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Error while updating marks. Problem in execution of sql query.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating marks");
            }
            return null;
        }
    }
}
