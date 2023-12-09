using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuentApplication.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }

        [ForeignKey(nameof(StudentAddress))]

        public int StudentAddressId { get; set; }

        // Add navigation property for one-to-one relationship
        public virtual StudentAddress? StudentAddress { get; set; }
    }
}
