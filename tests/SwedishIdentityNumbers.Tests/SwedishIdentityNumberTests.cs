﻿// ███████╗███████╗██████╗ ██╗███████╗
// ██╔════╝██╔════╝██╔══██╗██║██╔════╝
// ███████╗█████╗  ██║  ██║██║███████╗
// ╚════██║██╔══╝  ██║  ██║██║╚════██║
// ███████║███████╗██████╔╝██║███████║
// ╚══════╝╚══════╝╚═════╝ ╚═╝╚══════╝
// Copyright (C) 2014 - 2023 Sedis AB
// SwedishIdentityNumberTests.cs
// Visual Studio Project: SwedishIdentityNumbers.Tests
// Created: 2023-10-31
// Last touched: 2023-10-31

using System.Reflection;
using SwedishIdentityNumbers.Models;
using Xunit;

namespace SwedishIdentityNumbers.Tests;

public class SwedishIdentityNumberTests
{
    [Theory]
    [InlineData("198012128378", true)]
    [InlineData("19801212-8378", true)]
    [InlineData("19801212-837", false)]
    [InlineData("19801212-837A", false)]
    public void Constructor_ValidatesAndSanitizesCorrectly(string input, bool isValid)
    {
        if (isValid)
        {
            var identityNumber = new TestIdentityNumber(input);
            Assert.Equal("198012128378", identityNumber.Number);
        }
        else
        {
            Assert.Throws<FormatException>(() => new TestIdentityNumber(input));
        }
    }

    [Theory]
    [InlineData("1234567890", true)]
    [InlineData("9876543210", false)]
    public void ValidateLuhn_ValidatesCorrectly(string input, bool isValid)
    {
        var identityNumber = new TestIdentityNumber("198012128378");
        var validateLuhnMethod = identityNumber.GetType()
            .GetMethod("ValidateLuhn", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = (bool)validateLuhnMethod.Invoke(identityNumber, new object[] { input });

        Assert.Equal(isValid, result);
    }

    [Theory]
    [InlineData("16AB123456", "AB123456")]
    [InlineData("1234567890", "1234567890")]
    public void SanitizeNumber_SanitizesCorrectly(string input, string expected)
    {
        var identityNumber = new TestIdentityNumber("198012128378");
        var sanitizeNumberMethod = identityNumber.GetType()
            .GetMethod("SanitizeNumber", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = (string)sanitizeNumberMethod.Invoke(identityNumber, new object[] { input });

        Assert.Equal(expected, result);
    }

    private class TestIdentityNumber : SwedishIdentityNumber
    {
        public TestIdentityNumber(string number) : base(number)
        {
        }

        public override bool IsValidFormat(string number)
        {
            return number.Length == 12;
        }
    }
}