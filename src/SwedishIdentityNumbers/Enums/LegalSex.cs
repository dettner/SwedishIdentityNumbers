//  -----------------------------------------------------------------------
//  <copyright file="LegalSex.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

namespace SwedishIdentityNumbers.Enums;

/// <summary>
///     Specifies the legal sex of an individual. This information is
///     extracted from the Swedish Personal Identity Number.
/// </summary>
/// <remarks>
///     The legal sex is determined based on the ninth digit of the Personal Identity Number,
///     where an odd number represents male and an even number represents female.
///     This designation may not align with an individual's gender identity.
/// </remarks>
public enum LegalSex
{
    /// <summary>
    ///     Represents an unspecified or unknown legal sex.
    /// </summary>
    None = 0,
    Male = 1,
    Female = 2
}