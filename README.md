# Stravaig Clock

A set of packages to assist with automated tests when the current time is required in code.

For full documentation see: https://stravaig-projects.github.io/Stravaig.Clock/

## Quick Start

* In your application inject an `IClock` where needed.
* Add the `IClock` to your service collection.
* In your tests replace the clock with a `FakeClock`, and then assert that the date emitted by the FakeClock is the one used.

```csharp
public class MyService
{
  private readonly IClock _clock;
  public MyService(IClock clock)
  {
    _clock = clock;
  }

  public Thing CreateTheThing()
  {
    return new Thing()
    {
      DateCreatedAt = _clock.UtcNow;
    }
  }
}
```

And in the test:

```csharp
[TestFixture]
public class MyServiceTests
{
  [Test]
  public void ThingIsCreatedAtTheAppropriateTime()
  {
    var testTime = new DateTime(2023, 8, 12, 16, 16, 0, DateTimeKind.Utc);
    var fakeClock = FakeClock.SetTo(testTime);
    var service = new MyService(fakeClock);

    var thing = service.CreateTheThing();

    thing.DateCreatedAt.ShouldBe(testTime);
  }
}
```

## Packages


* Stable releases:
  * ![Nuget](https://img.shields.io/nuget/v/Stravaig.Clock?color=004880&label=nuget%20stable&logo=nuget) View [Stravaig.Clock](https://www.nuget.org/packages/Stravaig.Clock) on Nuget
  * ![Nuget](https://img.shields.io/nuget/v/Stravaig.Clock.Testing?color=004880&label=nuget%20stable&logo=nuget) View [Stravaig.Clock.Testing](https://www.nuget.org/packages/Stravaig.Clock.Testing) on Nuget
  * ![Nuget](https://img.shields.io/nuget/v/Stravaig.Clock.DependencyInjection?color=004880&label=nuget%20stable&logo=nuget) View [Stravaig.Clock.DependencyInjection](https://www.nuget.org/packages/Stravaig.Clock.DependencyInjection) on Nuget
* Latest releases:
  * ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Stravaig.Clock?color=ffffff&label=nuget%20latest&logo=nuget) View [Stravaig.Clock](https://www.nuget.org/packages/Stravaig.Clock) on Nuget
  * ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Stravaig.Clock.Testing?color=ffffff&label=nuget%20latest&logo=nuget) View [Stravaig.Clock.Testing](https://www.nuget.org/packages/Stravaig.Clock.Testing) on Nuget
  * ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Stravaig.Clock.DependencyInjection?color=ffffff&label=nuget%20latest&logo=nuget) View [Stravaig.Clock.DependencyInjection](https://www.nuget.org/packages/Stravaig.Clock.DependencyInjection) on Nuget
