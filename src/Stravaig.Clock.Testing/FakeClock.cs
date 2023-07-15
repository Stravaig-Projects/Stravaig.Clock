using System;

namespace Stravaig.Clock.Testing;

public class FakeClock : IClock
{
    public FakeClock()
    {
        SetTime(DateTime.UtcNow);
    }

    public FakeClock(DateTime time)
    {
        SetTime(time);
    }

    public static FakeClock Now => new();

    public static FakeClock SetTo(DateTime time) => new(time);

    public DateTime LocalNow { get; private set; }

    public DateTime UtcNow { get; private set; }
    
    public DateTime Today { get; private set; }

    public void SetTime(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Utc)
        {
            UtcNow = dateTime;
            LocalNow = dateTime.ToLocalTime();
        }
        else if (dateTime.Kind == DateTimeKind.Local)
        {
            LocalNow = dateTime;
            UtcNow = dateTime.ToUniversalTime();
        }
        else
        {
            throw new ArgumentException(
                $"The DateTime object must have the Kind property set to either Utc or Local. It is {dateTime.Kind}.",
                nameof(dateTime));
        }

        Today = LocalNow.Date;
    }
}