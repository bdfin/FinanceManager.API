using System;

namespace FinanceManager.Models
{
    public class BaseModel : IBaseModel
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public void Initialise()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
