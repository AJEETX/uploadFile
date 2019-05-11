using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BYO.Model
{
    public class InputModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Annual Salary")]
        public decimal AnnualSalary { get; set; }
        [Required]
        [DisplayName("Super Rate")]
        public decimal SuperRate { get; set; }
        [Required]
        [DisplayName("Payment Start Date")]
        public string PaymentStartDate { get; set; }
    }
    public class OutputModel
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Pay Period")]
        public string PayPeriod{ get; set; }
        [DisplayName("Gross Income")]
        public decimal GrossIncome { get; set; }
        [DisplayName("Incometax")]
        public decimal Incometax { get; set; }
        [DisplayName("Net Income")]
        public decimal NetIncome { get; set; }
        [DisplayName("Super")]
        public decimal Super { get; set; }
    }
}
