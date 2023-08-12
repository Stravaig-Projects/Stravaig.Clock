---
layout: default
title: Asserting with the Test Clock - Stravaig.Clock documentation
---
# Asserting with the Test Clock

If your code is not in a state where you can easily inject an `IClock` (see: [Using the injectable clock](using-the-injectable-clock.md)) you may find that the `DateTime` extention methiod `ShouldBeDuringTest(...)` is _good enough_ to assert that a `DateTime` object represents a time during the test run.

To use this technique, you must first add the (Stravaig.Clock.Testing)[https://www.nuget.org/packages/Stravaig.Clock.Testing] nuget package to your test project. Then you need to set up a `TestClock` during the "arrange" phase of the test, and then assert the relevant `DateTime` objects during the "assert" stage of the test.

e.g.

```csharp
[Test]
public void TestTheThing()
{
  // Arrange
  var clock = TestClock.Start();
  var thing = new Thing();

  // Act
  thing.DoStuff();

  // Assert
  thing.StuffHappenedAt.ShouldBeDuringTests(clock);
}
```

If the test fails the test running will show a message like this:
```
thing.StuffHappenedAt should be after the test started.
    The time should be between
2023-08-12T13:58:55.8078350+01:00
    and
2023-08-12T13:58:55.8089880+01:00
    but was
2023-08-12T13:58:55.8066560+01:00
```
or
```
thing.StuffHappenedAt should be before the test ended.
    The time should be between
2023-08-12T13:58:55.7799870+01:00
    and
2023-08-12T13:58:55.7811500+01:00
    but was
2023-08-12T13:58:55.7823020+01:00
```

The `TestClock` will work out what to do if the time given is local or UTC, however, if the time being asserted is of `DateTimeKind.Unspecfied` then you will need to specify a hint to the `ShouldBeDuringTest()` method so it can interpret the date properly.

e.g.
```
thing.StuffHappenedAt.ShouldBeDuringTests(clock, DateTimeKind.Utc);
```

If you don't specify the hint, then the test will fail with the message: 
```
thing.StuffHappenedAt has an unspecified kind. Unable to interpret 2023-08-12T12:58:55.8322840 as UTC or Local.
```