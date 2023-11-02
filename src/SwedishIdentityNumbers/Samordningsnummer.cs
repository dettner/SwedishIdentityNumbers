//  -----------------------------------------------------------------------
//  <copyright file="Samordningsnummer.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

namespace SwedishIdentityNumbers;

/// <summary>
///     Represents a Swedish coordination number (Samordningsnummer), which is used for individuals who are registered in
///     Sweden but do not have a personal identity number (Personnummer).
/// </summary>
public sealed class Samordningsnummer : Personnummer
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Samordningsnummer" /> class.
    /// </summary>
    /// <param name="number">The string representation of the Samordningsnummer.</param>
    /// <exception cref="FormatException">Thrown if the number format is invalid.</exception>
    public Samordningsnummer(string number) : base(number)
    {
    }

    /// <summary>
    ///     Attempts to create a new instance of the <see cref="Samordningsnummer" /> class.
    /// </summary>
    /// <param name="number">The string representation of the Samordningsnummer.</param>
    /// <param name="result">The resulting <see cref="Samordningsnummer" /> instance, if the creation succeeds.</param>
    /// <returns><c>true</c> if the creation succeeds; otherwise, <c>false</c>.</returns>
    public static bool TryCreate(string number, out Samordningsnummer? result)
    {
        try
        {
            result = new Samordningsnummer(number);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    ///     Adjusts the day component of the date in the Samordningsnummer by subtracting 60, as per the rules for
    ///     Samordningsnummer.
    /// </summary>
    /// <param name="day">The day component of the date in the Samordningsnummer.</param>
    /// <returns>The adjusted day component of the date.</returns>
    protected override int AdjustDay(int day)
    {
        return day - 60; // Adjust day for Samordningsnummer
    }
}