using StudentManagementAPI.Middleware;
using StudentManagementAPI.Models;
using StudentManagementAPI.Models.DTOs;
using StudentManagementAPI.Repositories;

namespace StudentManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository repository, ILogger<StudentService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync()
        {
            var students = await _repository.GetAllAsync();
            return students.Select(MapToDto);
        }

        public async Task<StudentResponseDto> GetStudentByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with id {id} was not found.");

            return MapToDto(student);
        }

        public async Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto dto)
        {
            if (await _repository.EmailExistsAsync(dto.Email))
            {
                throw new BadRequestException($"A student with email '{dto.Email}' already exists.");
            }

            var student = new Student
            {
                Name = dto.Name.Trim(),
                Email = dto.Email.Trim().ToLowerInvariant(),
                Age = dto.Age,
                Course = dto.Course.Trim(),
                CreatedDate = DateTime.UtcNow
            };

            await _repository.AddAsync(student);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Created student {StudentId} ({Email})", student.Id, student.Email);

            return MapToDto(student);
        }

        public async Task<StudentResponseDto> UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            var student = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with id {id} was not found.");

            if (await _repository.EmailExistsAsync(dto.Email, excludeId: id))
            {
                throw new BadRequestException($"A student with email '{dto.Email}' already exists.");
            }

            student.Name = dto.Name.Trim();
            student.Email = dto.Email.Trim().ToLowerInvariant();
            student.Age = dto.Age;
            student.Course = dto.Course.Trim();

            _repository.Update(student);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Updated student {StudentId}", student.Id);

            return MapToDto(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Student with id {id} was not found.");

            _repository.Delete(student);
            await _repository.SaveChangesAsync();

            _logger.LogInformation("Deleted student {StudentId}", id);
        }

        private static StudentResponseDto MapToDto(Student student)
        {
            return new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Course = student.Course,
                CreatedDate = student.CreatedDate
            };
        }
    }
}
