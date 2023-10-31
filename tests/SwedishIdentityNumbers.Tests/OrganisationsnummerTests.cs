// ███████╗███████╗██████╗ ██╗███████╗
// ██╔════╝██╔════╝██╔══██╗██║██╔════╝
// ███████╗█████╗  ██║  ██║██║███████╗
// ╚════██║██╔══╝  ██║  ██║██║╚════██║
// ███████║███████╗██████╔╝██║███████║
// ╚══════╝╚══════╝╚═════╝ ╚═╝╚══════╝
// Copyright (C) 2014 - 2023 Sedis AB
// OrganisationsnummerTests.cs
// Visual Studio Project: SwedishIdentityNumbers.Tests
// Created: 2023-10-31
// Last touched: 2023-10-31

using SwedishIdentityNumbers.Models;
using Xunit;

namespace SwedishIdentityNumbers.Tests;

public class OrganisationsnummerTests
{
    [Theory]
    [InlineData("556456-4252", true, SwedishCompanyForm.JointStockCompany)]
    [InlineData("915652-5173", true, SwedishCompanyForm.GeneralPartnership)]
    [InlineData("716920-5979", true, SwedishCompanyForm.HousingCooperative)]
    [InlineData("245000-4653", true, SwedishCompanyForm.GovernmentAgency)]
    [InlineData("126920-5979", true, SwedishCompanyForm.ReligiousCommunity)]
    [InlineData("316920-5979", true, SwedishCompanyForm.Unknown)]
    [InlineData("556456-4250", false, SwedishCompanyForm.Unknown)] // Invalid Luhn
    public void TryCreate_ValidatesCorrectly(string number, bool isValid, SwedishCompanyForm expectedForm)
    {
        var success = Organisationsnummer.TryCreate(number, out var result);

        Assert.Equal(isValid, success);
        if (isValid)
        {
            Assert.NotNull(result);
            Assert.Equal(number.Replace("-", ""), result!.Number);
            Assert.Equal(expectedForm, result!.ProbableSwedishCompanyForm);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Theory]
    [InlineData("556456-4252")]
    [InlineData("915652-5173")]
    [InlineData("716920-5979")]
    public void Constructor_CreatesInstance(string number)
    {
        var organisationsnummer = new Organisationsnummer(number);

        Assert.NotNull(organisationsnummer);
        Assert.Equal(number.Replace("-", ""), organisationsnummer.Number);
    }

    [Theory]
    [InlineData("556456-4250")]
    [InlineData("915652-5170")]
    [InlineData("716920-5970")]
    public void Constructor_ThrowsFormatException(string number)
    {
        Assert.Throws<FormatException>(() => new Organisationsnummer(number));
    }
}