//  -----------------------------------------------------------------------
//  <copyright file="LuhnValidator.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace SwedishIdentityNumbers.CheckDigitValidators;

/// <summary>
///     Implements the <see cref="ICheckDigitValidator" /> interface to provide Luhn algorithm based check digit
///     validation.
///     <remarks>
///         See the <see href="https://en.wikipedia.org/wiki/Luhn_algorithm">Luhn algorithm on Wikipedia</see>.
///     </remarks>
/// </summary>
public class LuhnValidator : ICheckDigitValidator
{
    /// <summary>
    ///     Validates the specified number using the Luhn algorithm.
    /// </summary>
    /// <param name="number">The number to validate.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="number" /> is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="number" /> is null.</exception>
    /// <exception cref="FormatException">Thrown if <paramref name="number" /> contains non-digit characters.</exception>
    /// <returns><c>true</c> if the specified number passes the Luhn validation; otherwise, <c>false</c>.</returns>
    public bool Validate(string number)
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
}