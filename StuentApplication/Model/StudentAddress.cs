// StudentAddress.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StuentApplication.Model
{
    public class StudentAddress
    {
        [Key]
        public int AddressId { get; set; }
        public string? Address { get; set; }

    }
}
