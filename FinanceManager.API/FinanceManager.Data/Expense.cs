namespace FinanceManager.Models
{
    public class Expense : BaseModel
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
