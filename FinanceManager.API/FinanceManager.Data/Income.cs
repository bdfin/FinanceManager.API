namespace FinanceManager.Models
{
    public class Income : BaseModel
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
