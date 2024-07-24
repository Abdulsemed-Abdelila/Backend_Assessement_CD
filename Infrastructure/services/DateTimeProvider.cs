using Application.Persistence.Contracts.Common;
namespace Infrastructure.services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CreateTime => DateTime.Now;
}
