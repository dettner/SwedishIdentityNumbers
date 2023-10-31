// ███████╗███████╗██████╗ ██╗███████╗
// ██╔════╝██╔════╝██╔══██╗██║██╔════╝
// ███████╗█████╗  ██║  ██║██║███████╗
// ╚════██║██╔══╝  ██║  ██║██║╚════██║
// ███████║███████╗██████╔╝██║███████║
// ╚══════╝╚══════╝╚═════╝ ╚═╝╚══════╝
// Copyright (C) 2014 - 2023 Sedis AB
// SwedishIdentityNumber.cs
// Visual Studio Project: SwedishIdentityNumbers
// Created: 2023-10-31
// Last touched: 2023-10-31

using System.Text.RegularExpressions;

namespace SwedishIdentityNumbers;

/// <summary>
///     Represents a Swedish Identity Number and provides a base for specialized identity number types.
/// </summary>
public abstract partial class SwedishIdentityNumber
{
    protected SwedishIdentityNumber(string number)
    {
        ArgumentException.ThrowIfNullOrEmpty(number, nameof(number));

        var sanitizedNumber = SanitizeNumber(number);

        if (!IsValidFormat(sanitizedNumber)) throw new FormatException("Invalid format.");
        Number = sanitizedNumber;
    }


    /// <summary>
    ///     Gets the identity number.
    /// </summary>
    public string Number { get; private set; }

    /// <summary>
    ///     Validates the format of the specified identity number.
    /// </summary>
    /// <param name="number">The identity number to validate.</param>
    /// <returns><c>true</c> if the specified number has a valid format; otherwise, <c>false</c>.</returns>
    public abstract bool IsValidFormat(string number);


    /// <summary>
    ///     Validates the specified number using the Luhn algorithm.
    /// </summary>
    /// <param name="number">The number to validate.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="number" /> is null or empty.</exception>
    /// <exception cref="FormatException">Thrown if <paramref name="number" /> contains non-digit characters.</exception>
    /// <returns><c>true</c> if the specified number passes the Luhn validation; otherwise, <c>false</c>.</returns>
    protected bool ValidateLuhn(string number)
    {
        ArgumentException.ThrowIfNullOrEmpty(number, nameof(number));
        if (!Regex.IsMatch(number, @"^\d+$"))
        {
            throw new FormatException($"Invalid format: {nameof(number)} contains non-digit characters.");
        }

        var sum = 0;
        var alternate = false;
        for (var i = number.Length - 1; i >= 0; i--)
        {
            var n = number[i] - '0';
            if (alternate)
            {
                n *= 2;
                if (n > 9)
                    n -= 9;
            }

            sum += n;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }

    /// <summary>
    ///     Cleans the specified number by removing non-digit characters and handling special prefixes.
    /// </summary>
    /// <param name="number">The number to clean.</param>
    /// <returns>The cleaned number.</returns>
    protected string SanitizeNumber(string number)
    {
        return NonDigit().Replace(number.StartsWith("16") ? number.Substring(2) : number, "");
    }

    [GeneratedRegex("\\D")]
    private static partial Regex NonDigit();
}