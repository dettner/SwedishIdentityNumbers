//  -----------------------------------------------------------------------
//  <copyright file="OrganisationsnummerTests.cs" company="Dettner Engineering AB">
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

public class OrganisationsnummerTests
{
    [Theory]
    [InlineData("556011-7482", true, SwedishCompanyForm.JointStockCompany)]
    [InlineData("916622-3959", true, SwedishCompanyForm.GeneralPartnership)]
    [InlineData("716408-5370", true, SwedishCompanyForm.HousingCooperative)]
    [InlineData("202100-5448", true, SwedishCompanyForm.GovernmentAgency)]
    [InlineData("252002-6614", true, SwedishCompanyForm.ReligiousCommunity)]
    [InlineData("802434-2153", false, SwedishCompanyForm.Unknown)] // Invalid Luhn
    [InlineData("5560117482", true, SwedishCompanyForm.JointStockCompany)]
    [InlineData("9166223959", true, SwedishCompanyForm.GeneralPartnership)]
    [InlineData("7164085370", true, SwedishCompanyForm.HousingCooperative)]
    [InlineData("2021005448", true, SwedishCompanyForm.GovernmentAgency)]
    [InlineData("2520026614", true, SwedishCompanyForm.ReligiousCommunity)]
    [InlineData("5564564250", false, SwedishCompanyForm.Unknown)] // Invalid Luhn
    [InlineData("16556011-7482", true, SwedishCompanyForm.JointStockCompany)]
    [InlineData("16916622-3959", true, SwedishCompanyForm.GeneralPartnership)]
    [InlineData("16716408-5370", true, SwedishCompanyForm.HousingCooperative)]
    [InlineData("16202100-5448", true, SwedishCompanyForm.GovernmentAgency)]
    [InlineData("16252002-6614", true, SwedishCompanyForm.ReligiousCommunity)]
    [InlineData("16556456-4250", false, SwedishCompanyForm.Unknown)] // Invalid Luhn
    [InlineData("165560117482", true, SwedishCompanyForm.JointStockCompany)]
    [InlineData("169166223959", true, SwedishCompanyForm.GeneralPartnership)]
    [InlineData("167164085370", true, SwedishCompanyForm.HousingCooperative)]
    [InlineData("162021005448", true, SwedishCompanyForm.GovernmentAgency)]
    [InlineData("162520026614", true, SwedishCompanyForm.ReligiousCommunity)]
    [InlineData("165564564250", false, SwedishCompanyForm.Unknown)] // Invalid Luhn
    public void TryCreate_ValidatesCorrectly(string number, bool isValid, SwedishCompanyForm expectedForm)
    {
        var success = Organisationsnummer.TryCreate(number, out var result);

        Assert.Equal(isValid, success);
        if (isValid)
        {
            Assert.NotNull(result);
            var input = number.Replace("-", "");
            if (input.StartsWith("16"))
            {
                input = input.Substring(2);
            }

            Assert.Equal(input, result!.Number);
            Assert.Equal(expectedForm, result!.ProbableSwedishCompanyForm);
        }
        else
        {
            Assert.Null(result);
        }
    }

    [Theory]
    [InlineData("556011-7482")]
    [InlineData("916622-3959")]
    [InlineData("716408-5370")]
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
    public void Constructor_ThrowsValidationException(string number)
    {
        Assert.Throws<ValidationException>(() => new Organisationsnummer(number));
    }
}