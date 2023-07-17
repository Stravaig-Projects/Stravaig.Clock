using System;

namespace Stravaig.Clock.Tests;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class ClockTests : IDisposable
{
    private readonly IDisposable _testTimeZone;

    public ClockTests()
    {
        // Chosen because it has an offset from UTC and does not have a daylight
        // savings time so the offset will be consistent year round.
        _testTimeZone = FakeLocalTimeZone.Set("Africa/Johannesburg");
    }

    public void Dispose()
    {
        _testTimeZone.Dispose();
        GC.SuppressFinalize(this);
    }

    [Test]
    public void LocalNowIsLocal()
    {
        var start = DateTime.Now;
        var testTime = Clock.Instance.LocalNow;
        var end = DateTime.Now;

        Console.WriteLine($"Test time is {testTime:O}");
        testTime.Kind.ShouldBe(DateTimeKind.Local);
        testTime.ShouldBeGreaterThanOrEqualTo(start);
        testTime.ShouldBeLessThanOrEqualTo(end);
    }

    [Test]
    public void UtcNowIsUtc()
    {
        var start = DateTime.UtcNow;
        var testTime = Clock.Instance.UtcNow;
        var end = DateTime.UtcNow;

        Console.WriteLine($"Test time is {testTime:O}");
        testTime.Kind.ShouldBe(DateTimeKind.Utc);
        testTime.ShouldBeGreaterThanOrEqualTo(start);
        testTime.ShouldBeLessThanOrEqualTo(end);
    }

    [Test]
    public void TodayIsTodayLocally()
    {
        var start = DateTime.Today;
        var testTime = Clock.Instance.Today;
        var end = DateTime.Today;

        Console.WriteLine($"Test time is {testTime:O}");
        testTime.Kind.ShouldBe(DateTimeKind.Local);
        testTime.ShouldBeGreaterThanOrEqualTo(start);
        testTime.ShouldBeLessThanOrEqualTo(end);
    }
}