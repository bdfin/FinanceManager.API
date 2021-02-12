using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FinanceManager.Models
{
    public class Household : BaseModel
    {
        public Household(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));

            UserId = userId;
            Name = "Household";
            People = new List<Person>();
        }

        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }

        public decimal TotalIncome 
        {
            get { return People.Sum(p => p.Income); }
        }

        public IList<Person> People { get; set; }
    }
}
