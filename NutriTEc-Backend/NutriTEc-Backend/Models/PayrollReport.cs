namespace NutriTEc_Backend.Models
{
    public class PayrollReport
    {
        public int ChargeType { get; set; }
        public string NutriEmail { get; set; }
        public string FullName { get; set; }
        public int CardNumber { get; set; }
        public int TotalAmount { get; set; }
        public double Discount { get; set; }
        public double ChargeAmount { get; set; }
    }
}
