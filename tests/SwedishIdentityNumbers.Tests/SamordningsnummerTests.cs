//  -----------------------------------------------------------------------
//  <copyright file="SamordningsnummerTests.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using SwedishIdentityNumbers.Enums;
using Xunit;

namespace SwedishIdentityNumbers.Tests;

public class SamordningsnummerTests
{
    [Theory]
    [InlineData("701063-2391", true)]
    [InlineData("701063+2391", true)]
    [InlineData("701063-2390", false)]
    [InlineData("7010632390", false)]
    [InlineData("701063+2390", false)]
    public void TryCreate_ValidatesCorrectly(string number, bool isValid)
    {
        var success = Samordningsnummer.TryCreate(number, out var result);

        Assert.Equal(isValid, success);
        if (isValid)
        {
            Assert.NotNull(result);
            Assert.Equal(number.Replace("-", "").Replace("+", ""), result!.Number);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Theory]
    [InlineData("701063-2391")]
    [InlineData("7010632391")]
    [InlineData("701063+2391")]
    public void Constructor_CreatesInstance(string number)
    {
        var samordningsnummer = new Samordningsnummer(number);

        Assert.NotNull(samordningsnummer);
        Assert.Equal(number.Replace("-", "").Replace("+", ""), samordningsnummer.Number);
    }

    [Theory]
    [InlineData("701063-2390")]
    [InlineData("7010632390")]
    [InlineData("701063+2390")]
    public void Constructor_ThrowsValidationException(string number)
    {
        Assert.Throws<ValidationException>(() => new Samordningsnummer(number));
    }


    [Theory]
    [InlineData("701063-2391", LegalSex.Male)]
    [InlineData("870566-5563", LegalSex.Female)]
    [InlineData("7010632391", LegalSex.Male)]
    [InlineData("8705665563", LegalSex.Female)]
    [InlineData("701063+2391", LegalSex.Male)]
    [InlineData("870566+5563", LegalSex.Female)]
    public void LegalSex_IsDeterminedCorrectly(string number, LegalSex expectedLegalSex)
    {
        var personnummer = new Samordningsnummer(number);

        Assert.Equal(expectedLegalSex, personnummer.LegalSex);
    }

    [Theory]
    [InlineData("701063-2391", LegalSex.Male)]
    [InlineData("870566-5563", LegalSex.Female)]
    [InlineData("7010632391", LegalSex.Male)]
    [InlineData("8705665563", LegalSex.Female)]
    [InlineData("701063+2391", LegalSex.Male)]
    [InlineData("870566+5563", LegalSex.Female)]
    public void TryCreate_SetsLegalSexCorrectly(string number, LegalSex expectedLegalSex)
    {
        var success = Samordningsnummer.TryCreate(number, out var result);

        Assert.True(success);
        Assert.NotNull(result);
        Assert.Equal(expectedLegalSex, result!.LegalSex);
    }
}