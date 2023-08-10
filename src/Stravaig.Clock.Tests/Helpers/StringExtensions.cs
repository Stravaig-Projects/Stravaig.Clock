using System;

namespace Stravaig.Clock.Tests.Helpers;

public static class StringExtensions
{
    public static string WriteToConsole(this string message)
    {
        Console.WriteLine(message);
        return message;
    }
}