//  -----------------------------------------------------------------------
//  <copyright file="SwedishIdentityNumber.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;
using SwedishIdentityNumbers.CheckDigitValidators;

namespace SwedishIdentityNumbers;

/// <summary>
///     Represents a Swedish Identity Number and provides a base for specialized identity number types.
/// </summary>
public abstract partial class SwedishIdentityNumber
{
    private readonly ICheckDigitValidator _checkDigitValidator;

    protected SwedishIdentityNumber(string number, ICheckDigitValidator? checkDigitValidator = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(number, nameof(number));

        var sanitizedNumber = SanitizeNumber(number);
        if (!IsValidFormat(sanitizedNumber)) throw new FormatException("Invalid format.");
        Number = sanitizedNumber;

        _checkDigitValidator = checkDigitValidator ?? new LuhnValidator();
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
    protected abstract bool IsValidFormat(string number);

    /// <summary>
    ///     Validates the specified identity number using the implemented validation strategy.
    /// </summary>
    /// <param name="number">The identity number to validate.</param>
    /// <returns><c>true</c> if the specified number is valid; otherwise, <c>false</c>.</returns>
    protected bool Validate(string number)
    {
        var okLength = number is { Length: 10 };

        return okLength && _checkDigitValidator.Validate(number);
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

    /// <summary>
    ///     Provides a compiled regular expression for matching non-digit characters.
    /// </summary>
    [GeneratedRegex("\\D")]
    private static partial Regex NonDigit();
}