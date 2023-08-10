using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Stravaig.Clock.Testing;
using Stravaig.Clock.Tests.Helpers;

namespace Stravaig.Clock.Tests.Testing;

[TestFixture]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Tests are not public.")]
public class TestClockTests
{
    private const string ShouldBeAfterTestStartedRegex =
        @"timeInTest should be after the test started\.(\r\n|\r|\n) {4}The time should be between(\r\n|\r|\n)\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7}(\+\d\d:\d\d|Z)(\r\n|\r|\n) {4}and(\r\n|\r|\n)\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7}(\+\d\d:\d\d|Z)(\r\n|\r|\n) {4}but was(\r\n|\r|\n)\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7}(\+\d\d:\d\d|Z)";

    private const string ShouldBeBeforeTestEndedRegex =
        @"timeInTest should be before the test ended\.(\r\n|\r|\n) {4}The time should be between(\r\n|\r|\n)\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7}(\+\d\d:\d\d|Z)(\r\n|\r|\n) {4}and(\r\n|\r|\n)\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7}(\+\d\d:\d\d|Z)(\r\n|\r|\n) {4}but was(\r\n|\r|\n)\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7}(\+\d\d:\d\d|Z)";

    private const string UnspecifiedKindRegex =
        @"timeInTest has an unspecified kind\. Unable to interpret \d{4}-\d\d-\d\dT\d\d:\d\d:\d\d\.\d{7} as UTC or Local\.";

    [Test]
    public void UtcHappyPath()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        var timeInTest = DateTime.UtcNow;
        Thread.Sleep(1);
        clock.Stop();
        timeInTest.ShouldBeDuringTest(clock);
    }

    [Test]
    public void UtcTimeIsBeforeTest()
    {
        var timeInTest = DateTime.UtcNow;
        Thread.Sleep(1);
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock))
            .Message.WriteToConsole().ShouldMatch(ShouldBeAfterTestStartedRegex);
    }

    [Test]
    public void UtcTimeIsAfterTest()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Thread.Sleep(1);
        var timeInTest = DateTime.UtcNow;
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock))
            .Message.WriteToConsole().ShouldMatch(ShouldBeBeforeTestEndedRegex);
    }

    [Test]
    public void LocalHappyPath()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        var timeInTest = DateTime.Now;
        Thread.Sleep(1);
        clock.Stop();
        timeInTest.ShouldBeDuringTest(clock);
    }

    [Test]
    public void LocalTimeIsBeforeTest()
    {
        var timeInTest = DateTime.Now;
        Thread.Sleep(1);
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock))
            .Message.WriteToConsole().ShouldMatch(ShouldBeAfterTestStartedRegex);
    }

    [Test]
    public void LocalTimeIsAfterTest()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Thread.Sleep(1);
        var timeInTest = DateTime.Now;
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock))
            .Message.WriteToConsole().ShouldMatch(ShouldBeBeforeTestEndedRegex);
    }

    [Test]
    public void UnspecifiedFailsBecauseNoAssumptionGiven()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        var timeInTest = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Unspecified);
        Thread.Sleep(1);
        clock.Stop();
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock))
            .Message.WriteToConsole().ShouldMatch(UnspecifiedKindRegex);
    }

    [Test]
    public void UnspecifiedAssumedUtcHappyPath()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        var timeInTest = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Unspecified);
        Thread.Sleep(1);
        clock.Stop();
        timeInTest.ShouldBeDuringTest(clock, DateTimeKind.Utc);
    }

    [Test]
    public void UnspecifiedAssumedLocalHappyPath()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        var timeInTest = new DateTime(DateTime.Now.Ticks, DateTimeKind.Unspecified);
        Thread.Sleep(1);
        clock.Stop();
        timeInTest.ShouldBeDuringTest(clock, DateTimeKind.Local);
    }

    [Test]
    public void UnspecifiedAssumedUtcTimeIsBeforeTest()
    {
        var timeInTest = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Unspecified);
        Thread.Sleep(1);
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock, DateTimeKind.Utc))
            .Message.WriteToConsole().ShouldMatch(ShouldBeAfterTestStartedRegex);
    }

    [Test]
    public void UnspecifiedAssumedUtcTimeIsAfterTest()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Thread.Sleep(1);
        var timeInTest = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Unspecified);
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock, DateTimeKind.Utc))
            .Message.WriteToConsole().ShouldMatch(ShouldBeBeforeTestEndedRegex);
    }

    [Test]
    public void UnspecifiedAssumedLocalTimeIsBeforeTest()
    {
        var timeInTest = new DateTime(DateTime.Now.Ticks, DateTimeKind.Unspecified);
        Thread.Sleep(1);
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock, DateTimeKind.Local))
            .Message.WriteToConsole().ShouldMatch(ShouldBeAfterTestStartedRegex);
    }

    [Test]
    public void UnspecifiedAssumedLocalTimeIsAfterTest()
    {
        var clock = TestClock.Start();
        Thread.Sleep(1);
        clock.Stop();
        Thread.Sleep(1);
        var timeInTest = new DateTime(DateTime.Now.Ticks, DateTimeKind.Unspecified);
        Should.Throw<TestClockException>(() => timeInTest.ShouldBeDuringTest(clock, DateTimeKind.Local))
            .Message.WriteToConsole().ShouldMatch(ShouldBeBeforeTestEndedRegex);
    }
}