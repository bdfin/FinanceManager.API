namespace FinanceManager.Models
{
    public class Expense : BaseModel
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
