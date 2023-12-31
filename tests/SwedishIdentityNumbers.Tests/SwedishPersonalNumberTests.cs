﻿//  -----------------------------------------------------------------------
//  <copyright file="SwedishPersonalNumberTests.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

using Xunit;

namespace SwedishIdentityNumbers.Tests;

public class SwedishIdentityNumberTests
{
    [Fact]
    public void Constructor_ValidNumber_SetsNumberProperty()
    {
        // Arrange
        var validNumber = "2021005448"; // Assume this is a valid number

        // Act
        var sut = new ConcreteSwedishIdentityNumber(
            validNumber); // Assume ConcreteSwedishIdentityNumber is a concrete implementation of SwedishIdentityNumber

        // Assert
        Assert.Equal(validNumber, sut.Number);
    }

    [Theory]
    [InlineData("")]
    public void Constructor_NullOrEmpty_ThrowsArgumentException(string invalidNumber)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ConcreteSwedishIdentityNumber(invalidNumber));
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_NullOrEmpty_ThrowsArgumentNullException(string invalidNumber)
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new ConcreteSwedishIdentityNumber(invalidNumber));
    }

    [Fact]
    public void Constructor_InvalidFormat_ThrowsFormatException()
    {
        // Arrange
        var invalidFormatNumber = "invalid";

        // Act & Assert
        Assert.Throws<FormatException>(() => new ConcreteSwedishIdentityNumber(invalidFormatNumber));
    }


    private class ConcreteSwedishIdentityNumber : SwedishIdentityNumber
    {
        public ConcreteSwedishIdentityNumber(string number) : base(number)
        {
        }

        protected override bool ValidateFormat(string number)
        {
            // Assume a simple length check for this example
            return number.Length == 10;
        }
    }
}