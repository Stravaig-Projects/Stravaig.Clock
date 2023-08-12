---
layout: default
title: Using the Injectable Clock - Stravaig.Clock documentation
---
# Using the Injectable Clock

## Setup your application

In the project where you set up your services, usually the entry project of your application, add the nuget package [Stravaig.Clock.DependencyInjection](https://www.nuget.org/packages/Stravaig.Clock.DependencyInjection)

In your `Startup` class, or where ever you build your `IServiceCollection`, add the following:

```csharp
services.AddClock();
```

## Using the Clock

In any project where you use the clock include the nuget package [Stravaig.Clock](https://www.nuget.org/packages/Stravaig.Clock/).

Inject the `IClock` in to your classes as you would any other dependency, e.g.

```csharp
public class MyService
{
  private readonly IClock _clock;
  public MyService(IClock clock)
  {
    _clock = clock;
  }
}
```

Then when you need to get the current time you can call the relevant method on `IClock`. e.g.

```csharp
public Thing CreateTheThing()
{
  var thing = new Thing()
  {
    DateCreatedUtc = _clock.UtcNow,
  };
}
```

## Testing code that uses the IClock

In your test project you will need to add the Nuget package [Stravaig.Clock.Testing](https://www.nuget.org/packages/Stravaig.Clock.Testing).

In your test you will need to create a `FakeClock` to inject into your class instead of using the regular `Clock` the application will use when running.

e.g.

```csharp
[Test]
public void TestTheThing()
{
  // Arrange
  var createTime = new DateTime(2023, 8, 12, 14, 20, 0, DateTimeKind.Utc);
  FakeClock fakeClock = FakeClock.SetTo(createTime);
  var myService = new MyService(fakeClock);

  // Act
  var thing = myService.CreateTheThing();

  // Assert
  thing.DateCreatedUtc.ShouldBe(createTime);
}
```

Note: The above test uses [Shouldly](https://github.com/shouldly/shouldly) as the assertion framework.