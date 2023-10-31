//  -----------------------------------------------------------------------
//  <copyright file="SwedishCompanyForm.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

namespace SwedishIdentityNumbers;

/// <summary>
///     (Swedish: Företagsform.)
///     In Sweden, there are seven different forms of companies, two of which are European company forms.
///     These forms differ in several ways, including the amount of
///     <see cref="https://sv.wikipedia.org/wiki/Kapital_(ekonomi)">capital</see> to invest,
///     the number of <see cref="https://sv.wikipedia.org/wiki/Del%C3%A4gare">shareholders</see>, the responsibility each
///     one has,
///     where the company's headquarters should be located, whether there's a need to protect the company's name in
///     multiple counties/countries, etc.
///     Although not generally considered company forms, businesses can also be conducted as
///     <see cref="https://sv.wikipedia.org/wiki/Ideell_f%C3%B6rening">non-profit associations</see>,
///     <see cref="https://sv.wikipedia.org/wiki/Enkelt_bolag">sole proprietorships</see>, or by
///     <see cref="https://sv.wikipedia.org/wiki/Privatperson">individuals</see>.
///     The
///     <see cref="https://bolagsverket.se/foretag/organisationsnummer.1207.html">prefix of the Organisationsnummer</see>
///     often indicates the probable form of a company.
/// </summary>
/// <remarks>
///     The enum values and the <see cref="Organisationsnummer.ProbableSwedishCompanyForm" /> property
///     implementation are
///     based on
///     information from
///     <see cref="https://bolagsverket.se/foretag/organisationsnummer.1207.html">Bolagsverket</see>.
/// </remarks>
/// <seealso cref="Organisationsnummer.ProbableSwedishCompanyForm" />
public enum SwedishCompanyForm
{
    /// <summary>
    ///     Aktiebolag, filialer, banker, försäkringsbolag och europabolag
    /// </summary>
    JointStockCompany,

    /// <summary>
    ///     Handelsbolag och kommanditbolag
    /// </summary>
    GeneralPartnership,

    /// <summary>
    ///     Bostadsrättsföreningar, ekonomiska föreningar, näringsdrivande ideella föreningar, bostadsföreningar, kooperativa
    ///     hyresrättsföreningar, europakooperativ och Europeiska grupperingar för territoriellt samarbete
    /// </summary>
    HousingCooperative,

    /// <summary>
    ///     Trossamfund
    /// </summary>
    ReligiousCommunity,

    /// <summary>
    ///     Statlig myndighet
    /// </summary>
    GovernmentAgency,

    /// <summary>
    ///     Okänd
    /// </summary>
    Unknown
}