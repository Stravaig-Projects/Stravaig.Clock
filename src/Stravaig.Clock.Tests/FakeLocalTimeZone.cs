using System;
using ReflectionMagic;

namespace Stravaig.Clock.Tests;

public class FakeLocalTimeZone : IDisposable
{
    private readonly TimeZoneInfo _actualLocalTimeZoneInfo;

    private static void SetLocalTimeZone(TimeZoneInfo timeZoneInfo)
    {
        Console.WriteLine($"Setting Timezone to {timeZoneInfo.DisplayName}.");
        typeof(TimeZoneInfo).AsDynamicType().s_cachedData._localTimeZone = timeZoneInfo;
    }

    public static FakeLocalTimeZone Set(string name)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(name);
        return new FakeLocalTimeZone(tz);
    }

    private FakeLocalTimeZone(TimeZoneInfo timeZoneInfo)
    {
        _actualLocalTimeZoneInfo = TimeZoneInfo.Local;
        SetLocalTimeZone(timeZoneInfo);
    }

    public void Dispose()
    {
        Console.WriteLine($"Resetting Timezone to {_actualLocalTimeZoneInfo.DisplayName}.");
        SetLocalTimeZone(_actualLocalTimeZoneInfo);
        GC.SuppressFinalize(this);
    }
}