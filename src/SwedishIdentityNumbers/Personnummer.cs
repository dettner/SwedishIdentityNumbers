//  -----------------------------------------------------------------------
//  <copyright file="Personnummer.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace SwedishIdentityNumbers;

/// <summary>
///     Represents a Swedish Personal Identity Number (Personnummer).
/// </summary>
public class Personnummer : SwedishIdentityNumber
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Personnummer" /> class.
    /// </summary>
    /// <param name="number">The string representation of the personal identity number.</param>
    /// <exception cref="FormatException">Thrown if the number format is invalid.</exception>
    public Personnummer(string number) : base(number)
    {
        DateOfBirth = ExtractDateOfBirth(number);
    }


    /// <summary>
    ///     Gets the date of birth extracted from the personal identity number.
    /// </summary>
    public DateOnly DateOfBirth { get; private set; }

    /// <summary>
    ///     Attempts to create a new instance of the <see cref="Personnummer" /> class.
    /// </summary>
    /// <param name="number">The string representation of the personal identity number.</param>
    /// <param name="result">The resulting <see cref="Personnummer" /> instance, if the creation succeeds.</param>
    /// <returns><c>true</c> if the creation succeeds; otherwise, <c>false</c>.</returns>
    public static bool TryCreate(string number, out Personnummer? result)
    {
        try
        {
            result = new Personnummer(number);
            return true;
        }
        catch (FormatException)
        {
            result = null;
            return false;
        }
    }


    /// <summary>
    ///     Determines whether the specified number has a valid format.
    /// </summary>
    /// <param name="number">The number to validate.</param>
    /// <returns><c>true</c> if the specified number has a valid format; otherwise, <c>false</c>.</returns>
    protected override bool IsValidFormat(string number)
    {
        number = SanitizeNumber(number);
        return number.Length == 10 && Validate(number) && ValidateDate(number);
    }

    private bool ValidateDate(string number)
    {
        return TryParseAndAdjustDate(number, out _);
    }

    private DateOnly ExtractDateOfBirth(string number)
    {
        TryParseAndAdjustDate(number, out var birthDate);
        return birthDate;
    }

    private bool TryParseAndAdjustDate(string number, out DateOnly date)
    {
        var day = AdjustDay(int.Parse(number.Substring(4, 2)));
        return DateOnly.TryParseExact($"{number[..4]}{day:D2}", "yyMMdd", null, DateTimeStyles.None,
            out date);
    }

    /// <summary>
    ///     Adjusts the day component of the date in the personal identity number.
    /// </summary>
    /// <param name="day">The day component of the date.</param>
    /// <returns>The adjusted day component of the date.</returns>
    protected virtual int AdjustDay(int day)
    {
        return day; // No adjustment for regular Personnummer
    }
}