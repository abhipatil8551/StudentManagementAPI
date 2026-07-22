using StudentManagementAPI.Models;

namespace StudentManagementAPI.Repositories
{
    // Repository handles ONLY data access (EF Core). No business rules live here.
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<bool> EmailExistsAsync(string email, int? excludeId = null);
        Task AddAsync(Student student);
        void Update(Student student);
        void Delete(Student student);
        Task<bool> SaveChangesAsync();
    }
}
