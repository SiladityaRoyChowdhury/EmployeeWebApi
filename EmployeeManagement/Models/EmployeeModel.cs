using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter first name"), MaxLength(30)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter last name"), MaxLength(30)]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Address")]
        public string EmployeeAddress { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter salary")]
        public decimal Salary { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Mobile number"), MaxLength(13)]
        public string MobileNumber { get; set; }
    }
}