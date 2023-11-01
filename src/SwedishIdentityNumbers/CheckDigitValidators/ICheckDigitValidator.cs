//  -----------------------------------------------------------------------
//  <copyright file="ICheckDigitValidator.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

namespace SwedishIdentityNumbers.CheckDigitValidators;

/// <summary>
///     Provides an interface for validating the check digit(s) of a given number.
/// </summary>
public interface ICheckDigitValidator
{
    /// <summary>
    ///     Validates the check digit(s) of the specified number.
    /// </summary>
    /// <param name="number">The number whose check digit(s) are to be validated.</param>
    /// <returns><c>true</c> if the specified number has valid check digit(s); otherwise, <c>false</c>.</returns>
    bool Validate(string number);
}