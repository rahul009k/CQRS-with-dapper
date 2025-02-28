using System.ComponentModel.DataAnnotations;

namespace CQRS_with_dapper.Models
{
    public class FarmerDto
    {
        [Required]
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone_number { get; set; } = "";
    }
}
