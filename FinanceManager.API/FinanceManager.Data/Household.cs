using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Models
{
    public class Household : BaseModel
    {
        public Household(string userId)
        {
            UserId = userId;
            Name = "Household";
            People = new List<Person>();
        }

        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }

        public IList<Person> People { get; set; }
    }
}
