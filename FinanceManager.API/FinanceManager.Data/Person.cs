using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Models
{
    public class Person : BaseModel
    {
        public Person()
        {
            Expenses = new List<Expense>();
        }

        [Required]
        public string Name { get; set; }

        public decimal Income { get; set; }

        public IList<Expense> Expenses { get; set; }
    }
}
