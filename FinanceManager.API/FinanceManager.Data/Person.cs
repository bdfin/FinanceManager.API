using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceManager.Models
{
    public class Person : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<Expense> Expenses { get; set; }
        public IList<Income> Income { get; set; }
    }
}
