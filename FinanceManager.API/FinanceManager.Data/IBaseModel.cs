using System;

namespace FinanceManager.Models
{
    public interface IBaseModel
    {
        string Id { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset UpdatedAt { get; set; }
    }
}
