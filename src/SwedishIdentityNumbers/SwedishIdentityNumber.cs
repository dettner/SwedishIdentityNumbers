//  -----------------------------------------------------------------------
//  <copyright file="SwedishIdentityNumber.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SwedishIdentityNumbers.CheckDigitValidators;

namespace SwedishIdentityNumbers;

/// <summary>
///     Represents a Swedish Identity Number and provides a base for specialized identity number types.
/// </summary>
public abstract partial class SwedishIdentityNumber
{
    private readonly ICheckDigitValidator _checkDigitValidator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="SwedishIdentityNumber" /> class.
    /// </summary>
    /// <param name="number">The string representation of the identity number.</param>
    /// <param name="checkDigitValidator">
    ///     An optional <see cref="ICheckDigitValidator" /> instance used for
    ///     validating the check digit of the identity number.
    ///     If not provided, a default <see cref="LuhnValidator" /> instance will be used.
    /// </param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="number" /> is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="number" /> is null.</exception>
    /// <exception cref="FormatException">Thrown if <paramref name="number" /> does not have a valid format.</exception>
    /// <exception cref="ValidationException">Thrown if <paramref name="number" /> does not have a correct check digit.</exception>
    protected SwedishIdentityNumber(string number, ICheckDigitValidator? checkDigitValidator = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(number, nameof(number));

        _checkDigitValidator = checkDigitValidator ?? new LuhnValidator();

        var sanitizedNumber = SanitizeNumber(number);
        if (!ValidateFormat(sanitizedNumber)) throw new FormatException("Invalid format.");
        if (!ValidateCheckDigit(sanitizedNumber)) throw new ValidationException("Invalid check digit.");
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
    protected virtual bool ValidateFormat(string number)
    {
        return number is { Length: 10 };
    }

    /// <summary>
    ///     Validates the check digit of the specified identity number.
    /// </summary>
    /// <param name="number">The identity number to validate.</param>
    /// <returns><c>true</c> if the specified number has a valid check digit; otherwise, <c>false</c>.</returns>
    private bool ValidateCheckDigit(string number)
    {
        return _checkDigitValidator.Validate(number);
    }


    /// <summary>
    ///     Cleans the specified number by removing non-digit characters and handling special prefixes.
    /// </summary>
    /// <param name="number">The number to clean.</param>
    /// <returns>The cleaned number.</returns>
    private string SanitizeNumber(string number)
    {
        return NonDigit().Replace(number.StartsWith("16") ? number.Substring(2) : number, "");
    }

    /// <summary>
    ///     Provides a compiled regular expression for matching non-digit characters.
    /// </summary>
    [GeneratedRegex("\\D")]
    private static partial Regex NonDigit();
}