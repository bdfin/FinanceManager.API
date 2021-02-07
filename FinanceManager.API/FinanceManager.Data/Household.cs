using System.Collections.Generic;

namespace FinanceManager.Models
{
    public class Household : BaseModel
    {
        public Household(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
        public IList<Person> People { get; set; }
    }
}
