//  -----------------------------------------------------------------------
//  <copyright file="LuhnValidatorTests.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using SwedishIdentityNumbers.CheckDigitValidators;
using Xunit;

namespace SwedishIdentityNumbers.Tests;

public class LuhnValidatorTests
{
    private readonly ICheckDigitValidator _validator = new LuhnValidator();

    [Theory]
    [InlineData("79927398713", true)] // Known valid Luhn number
    [InlineData("1234567812345670", true)] // Another known valid Luhn number
    [InlineData("79927398710", false)] // Known invalid Luhn number (last digit changed)
    [InlineData("1234567812345671", false)] // Another known invalid Luhn number (last digit changed)
    public void Validate_ValidAndInvalidLuhnNumbers_ReturnsExpectedResult(string number, bool expectedResult)
    {
        // Act
        var result = _validator.Validate(number);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(null)]
    public void Validate_Null_ThrowsArgumentNullException(string invalidInput)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _validator.Validate(invalidInput));
        Assert.Equal("number", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    public void Validate_Empty_ThrowsArgumentException(string invalidInput)
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(invalidInput));
        Assert.Equal("number", exception.ParamName);
    }

    [Theory]
    [InlineData("7992739871a")]
    [InlineData("123456781234567x")]
    public void Validate_NonDigitCharacters_ThrowsFormatException(string invalidInput)
    {
        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _validator.Validate(invalidInput));
        Assert.Equal("Invalid format: number contains non-digit characters.", exception.Message);
    }

    [Fact]
    public void Validate_SingleDigit_ThrowsFormatException()
    {
        // A single digit cannot satisfy the Luhn algorithm

        // Arrange
        var singleDigit = "7";

        // Act
        Assert.Throws<FormatException>(() => _validator.Validate(singleDigit));
    }
}