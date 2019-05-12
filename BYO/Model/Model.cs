using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BYO.Model
{
    public class InputModel
    {
        [Required]
        [JsonProperty("first name")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty("last name")]
        public string LastName { get; set; }
        [Required]
        [JsonProperty("annual salary")]
        public decimal AnnualSalary { get; set; }
        [Required]
        [JsonProperty("super rate")]
        public decimal SuperRate { get; set; }
        [Required]
        [JsonProperty("payment start date")]
        public string PaymentStartDate { get; set; }
    }
    public class OutputModel
    {
        public string Name { get; set; }
        [JsonProperty("pay period")]
        public string PayPeriod{ get; set; }
        [JsonProperty("gross income")]
        public decimal GrossIncome { get; set; }
        [JsonProperty("income tax")]
        public decimal Incometax { get; set; }
        [JsonProperty("net income")]
        public decimal NetIncome { get; set; }
        public decimal Super { get; set; }
    }
}
