// ███████╗███████╗██████╗ ██╗███████╗
// ██╔════╝██╔════╝██╔══██╗██║██╔════╝
// ███████╗█████╗  ██║  ██║██║███████╗
// ╚════██║██╔══╝  ██║  ██║██║╚════██║
// ███████║███████╗██████╔╝██║███████║
// ╚══════╝╚══════╝╚═════╝ ╚═╝╚══════╝
// Copyright (C) 2014 - 2023 Sedis AB
// Organisationsnummer.cs
// Visual Studio Project: SwedishIdentityNumbers
// Created: 2023-10-31
// Last touched: 2023-10-31

using System.Text.RegularExpressions;

namespace SwedishIdentityNumbers;

/// <summary>
///     Represents a Swedish organization number (Organisationsnummer), which is used to identify legal entities in Sweden.
/// </summary>
public sealed partial class Organisationsnummer : SwedishIdentityNumber
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Organisationsnummer" /> class.
    /// </summary>
    /// <param name="number">The string representation of the organization number.</param>
    /// <exception cref="FormatException">Thrown if the number format is invalid.</exception>
    public Organisationsnummer(string number) : base(number)
    {
        ProbableSwedishCompanyForm = GetProbableSwedishCompanyForm(number);
    }

    /// <summary>
    ///     Gets the probable form of the company based on the prefix of the organization number.
    /// </summary>

    public SwedishCompanyForm ProbableSwedishCompanyForm { get; private set; }

    private SwedishCompanyForm GetProbableSwedishCompanyForm(string number)
    {
        SwedishCompanyForm swedishCompanyForm;

        var prefix = number[..2];
        if (GovernmentAgencyRegex().IsMatch(prefix))
        {
            swedishCompanyForm = SwedishCompanyForm.GovernmentAgency;
        }

        swedishCompanyForm = number[..1] switch
        {
            "5" => SwedishCompanyForm.JointStockCompany,
            "9" => SwedishCompanyForm.GeneralPartnership,
            "7" or "8" => SwedishCompanyForm.HousingCooperative,
            "2" => SwedishCompanyForm.ReligiousCommunity,
            _ => SwedishCompanyForm.Unknown
        };
        return swedishCompanyForm;
    }

    /// <summary>
    ///     Attempts to create a new instance of the <see cref="Organisationsnummer" /> class.
    /// </summary>
    /// <param name="number">The string representation of the organization number.</param>
    /// <param name="result">The resulting <see cref="Organisationsnummer" /> instance, if the creation succeeds.</param>
    /// <returns><c>true</c> if the creation succeeds; otherwise, <c>false</c>.</returns>
    public static bool TryCreate(string number, out Organisationsnummer? result)
    {
        try
        {
            result = new Organisationsnummer(number);
            return true;
        }
        catch (FormatException)
        {
            result = null;
            return false;
        }
    }

    /// <summary>
    ///     Validates the format of the specified organization number.
    /// </summary>
    /// <param name="number">The organization number to validate.</param>
    /// <returns><c>true</c> if the specified number is a valid organization number format; otherwise, <c>false</c>.</returns>
    public override bool IsValidFormat(string number)
    {
        number = SanitizeNumber(number);
        return number.Length == 10 && ValidateLuhn(number);
    }


    /// <summary>
    ///     A regex pattern used to identify government agencies based on the prefix of the organization number.
    /// </summary>
    [GeneratedRegex("^2[0-9]")]
    private static partial Regex GovernmentAgencyRegex();
}