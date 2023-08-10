using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Stravaig.Clock.Testing;

public static class DateTimeExtensions
{
    public static void ShouldBeDuringTest(
        this DateTime actual,
        TestClock expected,
        DateTimeKind assumedKindIfUnspecified = DateTimeKind.Unspecified,
        [CallerArgumentExpression(nameof(actual))] string? argExpr = null)
    {
        var originalKind = actual.Kind;
        if (actual.Kind == DateTimeKind.Local)
        {
            actual = actual.ToUniversalTime();
        }
        else if (actual.Kind == DateTimeKind.Unspecified)
        {
            ThrowIfUnspecified(actual, assumedKindIfUnspecified, argExpr);
            actual = new DateTime(actual.Ticks, assumedKindIfUnspecified).ToUniversalTime();
            originalKind = assumedKindIfUnspecified;
        }

        ThrowIfOutOfRange(actual, expected, argExpr, originalKind);
    }

    private static void ThrowIfOutOfRange(DateTime actual, TestClock expected, string? argExpr, DateTimeKind kind)
    {
        if (actual < expected.StartTime)
        {
            var msg = BuildMessage(argExpr, "after the test started.", actual, expected.StartTime, expected.EndTime, kind);
            throw new TestClockException(msg);
        }

        // End time can move, if clock not stopped.
        var endTime = expected.EndTime;

        if (actual > endTime)
        {
            var msg = BuildMessage(argExpr, "before the test ended.", actual, expected.StartTime, expected.EndTime, kind);
            throw new TestClockException(msg);
        }
    }

    private static string BuildMessage(string? argExpr, string fragment, DateTime actual, DateTime startTime, DateTime endTime, DateTimeKind kind)
    {
        if (kind == DateTimeKind.Local)
        {
            startTime = startTime.ToLocalTime();
            endTime = endTime.ToLocalTime();
            actual = actual.ToLocalTime();
        }

        StringBuilder sb = new(256);
        sb.Append(argExpr ?? "The actual time");
        sb.Append(" should be ");
        sb.AppendLine(fragment);

        sb.AppendLine("    The time should be between");
        sb.AppendLine(startTime.ToString("O"));
        sb.AppendLine("    and");
        sb.AppendLine(endTime.ToString("O"));
        sb.AppendLine("    but was");
        sb.Append(actual.ToString("O"));
        return sb.ToString();
    }


    private static void ThrowIfUnspecified(DateTime actual, DateTimeKind assumed, string? argExpr)
    {
        if (assumed == DateTimeKind.Unspecified)
        {
            argExpr ??= "The actual time";
            throw new TestClockException(
                $"{argExpr} has an unspecified kind. Unable to interpret {actual:O} as UTC or Local.");
        }
    }
}