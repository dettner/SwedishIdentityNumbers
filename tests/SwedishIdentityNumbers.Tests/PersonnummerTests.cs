// ███████╗███████╗██████╗ ██╗███████╗
// ██╔════╝██╔════╝██╔══██╗██║██╔════╝
// ███████╗█████╗  ██║  ██║██║███████╗
// ╚════██║██╔══╝  ██║  ██║██║╚════██║
// ███████║███████╗██████╔╝██║███████║
// ╚══════╝╚══════╝╚═════╝ ╚═╝╚══════╝
// Copyright (C) 2014 - 2023 Sedis AB
// PersonnummerTests.cs
// Visual Studio Project: SwedishIdentityNumbers.Tests
// Created: 2023-10-31
// Last touched: 2023-10-31

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