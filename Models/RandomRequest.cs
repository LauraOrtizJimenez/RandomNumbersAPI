namespace RandomNumbersAPI.Models
{
    public class RandomRequest
    {
        public string Type { get; set; } = "number"; // number | decimal | string
        public int? Min { get; set; }                // aplica solo para number
        public int? Max { get; set; }                // aplica solo para number
        public int Decimals { get; set; } = 2;      // aplica solo para decimal
        public int Length { get; set; } = 8;        // aplica solo para string
    }
}