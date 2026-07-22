using StudentManagementAPI.Models.DTOs;

namespace StudentManagementAPI.Services
{
    // Service layer holds business rules (validation, duplicate checks, mapping)
    // and is what the controller talks to — it never touches EF Core directly.
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponseDto>> GetAllStudentsAsync();
        Task<StudentResponseDto> GetStudentByIdAsync(int id);
        Task<StudentResponseDto> CreateStudentAsync(CreateStudentDto dto);
        Task<StudentResponseDto> UpdateStudentAsync(int id, UpdateStudentDto dto);
        Task DeleteStudentAsync(int id);
    }
}
