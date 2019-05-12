using Newtonsoft.Json;

namespace BYO.Model
{
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
