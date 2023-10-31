//  -----------------------------------------------------------------------
//  <copyright file="PersonnummerTests.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using Xunit;

namespace SwedishIdentityNumbers.Tests;

public class PersonnummerTests
{
    [Theory]
    [InlineData("870506-5555", true)]
    [InlineData("8705065555", true)]
    [InlineData("870506+5555", true)]
    [InlineData("870506-5550", false)]
    [InlineData("8705065550", false)]
    [InlineData("870506+5550", false)]
    public void TryCreate_ValidatesCorrectly(string number, bool isValid)
    {
        var success = Personnummer.TryCreate(number, out var result);

        Assert.Equal(isValid, success);
        if (isValid)
        {
            Assert.NotNull(result);
            Assert.Equal(number.Replace("-", "").Replace("+", ""), result!.Number);
            Assert.Equal(new DateOnly(1987, 05, 06), result!.DateOfBirth);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Theory]
    [InlineData("870506-5555")]
    [InlineData("8705065555")]
    [InlineData("870506+5555")]
    public void Constructor_CreatesInstance(string number)
    {
        var personnummer = new Personnummer(number);

        Assert.NotNull(personnummer);
        Assert.Equal(number.Replace("-", "").Replace("+", ""), personnummer.Number);
        Assert.Equal(new DateOnly(1987, 05, 06), personnummer.DateOfBirth);
    }

    [Theory]
    [InlineData("870506-5550")]
    [InlineData("8705065550")]
    [InlineData("870506+5550")]
    public void Constructor_ThrowsFormatException(string number)
    {
        Assert.Throws<FormatException>(() => new Personnummer(number));
    }
}