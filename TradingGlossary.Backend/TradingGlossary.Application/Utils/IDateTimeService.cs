namespace MarketingLvs.Application.Utils;

public interface IDateTimeService
{
	DateTime Now { get; } 
	DateTime Today { get; }
}