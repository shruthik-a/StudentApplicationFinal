using Microsoft.EntityFrameworkCore;
using StuentApplication.Data;
using StuentApplication.Model;

namespace StuentApplication.Repository
{
   
    
        public class StudentRepository
        {
            private readonly ApplicationDbContext databaseContext;
            public StudentRepository(ApplicationDbContext databaseContext)
            {
                this.databaseContext = databaseContext;
            }

            public List<Student> GetStudents()
            {
                return databaseContext.Students.Include(s => s.StudentAddress).ToList();
            }

            public Student GetStudentById(int id)
            {
                return databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
            }

            public void CreateStudent(Student student)
            {
                databaseContext.Add(student);
                databaseContext.SaveChanges();
            }

            public void UpdateStudent(int id, Student updatedStudent)
            {
                Student existingStudent = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
                if (existingStudent != null)
                {
                    existingStudent.Name = updatedStudent.Name;
                    existingStudent.Department = updatedStudent.Department;
                    existingStudent.StudentAddress.Address = updatedStudent.StudentAddress.Address;

                    databaseContext.SaveChanges();
                }

            }

            public void PatchStudent(int id, Student student)
            {
                var existingStudent = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
                if (existingStudent.Name != student.Name && student.Name != null)
                {
                    existingStudent.Name = student.Name;
                }
                if (existingStudent.Department != student.Department && student.Department != null)
                {
                    existingStudent.Department = student.Department;
                }
                if (existingStudent.StudentAddress != student.StudentAddress && student.StudentAddress != null)
                {
                    existingStudent.StudentAddress = student.StudentAddress;
                }
                databaseContext.SaveChanges();
            }

            public void DeleteStudent(int id)
            {
                Student student = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
                if (student.StudentAddress != null)
                {
                    databaseContext.StudentAddresses.Remove(student.StudentAddress);
                }
                databaseContext.Students.Remove(student);
                databaseContext.SaveChanges();
            }

            public List<Student> searchStudents(string searchItem)
            {
                return databaseContext.Students
                    .Where(s =>
                        s.Name.Contains(searchItem) ||
                        s.Department.Contains(searchItem) ||
                        s.StudentAddress.Address.Contains(searchItem))
                     .OrderBy(s => s.Name)
                     .ThenBy(s => s.Department)
                     .ThenBy(s => s.StudentAddress.Address)
                    .Include(s => s.StudentAddress)
                    .ToList();
            }
        public Dictionary<string, List<Student>> GroupByDepartment()
        {
            var studentsWithAddress = databaseContext.Students
                .Include(s => s.StudentAddress)
                .ToList();

            var groupedResults = studentsWithAddress
                .GroupBy(s => s.Department)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(s => new Student
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Department = s.Department,
                        StudentAddressId = s.StudentAddressId,
                        StudentAddress = new StudentAddress
                        {
                            AddressId = s.StudentAddress.AddressId,
                            Address = s.StudentAddress.Address
                        }
                    }).ToList()
                );

            return groupedResults;
        }



    }
}

