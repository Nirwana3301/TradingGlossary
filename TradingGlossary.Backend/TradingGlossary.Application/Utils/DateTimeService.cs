namespace MarketingLvs.Application.Utils;

public class DateTimeService : IDateTimeService
{
	public DateTime Now => DateTime.UtcNow;
	public DateTime Today => DateTime.UtcNow.Date;
}