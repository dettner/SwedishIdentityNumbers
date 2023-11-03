//  -----------------------------------------------------------------------
//  <copyright file="SwedishCompanyForm.cs" company="Dettner Engineering AB">
//      Copyright (c) 2023 Dettner Engineering AB. All rights reserved.
// 
//      This file is part of SwedishIdentityNumbers project.
//      Licensed under the MIT License. See LICENSE.txt in the project root for license information.
//  </copyright>
// -----------------------------------------------------------------------

namespace SwedishIdentityNumbers.Enums;

/// <summary>
///     (Swedish: Företagsform.)
///     In Sweden, there are seven different forms of companies, two of which are European company forms.
///     These forms differ in several ways, including the amount of
///     <see href="https://sv.wikipedia.org/wiki/Kapital_(ekonomi)">capital</see> to invest,
///     the number of <see href="https://sv.wikipedia.org/wiki/Del%C3%A4gare">shareholders</see>,
///     the responsibility each one has, where the company's headquarters should be
///     located, whether there's a need to protect the company's name in multiple counties/countries, etc.
///     Although not generally considered company forms, businesses can also be conducted as
///     <see href="https://sv.wikipedia.org/wiki/Ideell_f%C3%B6rening">non-profit associations</see>,
///     <see href="https://sv.wikipedia.org/wiki/Enkelt_bolag">sole proprietorships</see>, or by
///     <see href="https://sv.wikipedia.org/wiki/Privatperson">individuals</see>.
///     The
///     <see href="https://bolagsverket.se/foretag/organisationsnummer.1207.html">prefix of the Organisationsnummer</see>
///     often indicates the probable form of a company.
/// </summary>
/// <remarks>
///     The enum values and the <see cref="Organisationsnummer.ProbableSwedishCompanyForm" /> property
///     implementation are based on information from
///     <see href="https://bolagsverket.se/foretag/organisationsnummer.1207.html">Bolagsverket</see>.
/// </remarks>
/// <seealso cref="Organisationsnummer.ProbableSwedishCompanyForm" />
public enum SwedishCompanyForm
{
    /// <summary>
    ///     <lang name="sv">Aktiebolag, filial, bank, försäkringsbolag och europabolag</lang><br />
    ///     <lang name="en">Joint-stock company, branch, bank, insurance company, and European company</lang>
    /// </summary>
    JointStockCompany = 5,

    /// <summary>
    ///     <lang name="sv">Handelsbolag och kommanditbolag</lang><br />
    ///     <lang name="en">Trading partnership and limited partnership</lang>
    /// </summary>
    GeneralPartnership = 9,

    /// <summary>
    ///     <lang name="sv">
    ///         Bostadsrättsförening, ekonomisk förening, näringsdrivande ideell förening, bostadsförening,
    ///         kooperativ hyresrättsförening, europakooperativ och Europeisk gruppering för territoriellt samarbete
    ///     </lang>
    ///     <br />
    ///     <lang name="en">
    ///         Housing cooperative, economic association, business-driven non-profit association, housing association,
    ///         cooperative rental association, European cooperative, and European Grouping for Territorial Cooperation
    ///     </lang>
    /// </summary>
    HousingCooperative = 7, // Using 7. 8 has same meaning

    /// <summary>
    ///     <lang name="sv">Trossamfund</lang><br />
    ///     <lang name="en">Religious community</lang>
    /// </summary>
    ReligiousCommunity = 2,

    /// <summary>
    ///     <lang name="sv">Statlig myndighet</lang><br />
    ///     <lang name="en">Governmental agency</lang>
    /// </summary>
    GovernmentAgency = 20,

    /// <summary>
    ///     <lang name="sv">Okänd</lang><br />
    ///     <lang name="en">Unknown</lang>
    /// </summary>
    Unknown = 0
}