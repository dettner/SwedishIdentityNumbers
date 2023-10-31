// ███████╗███████╗██████╗ ██╗███████╗
// ██╔════╝██╔════╝██╔══██╗██║██╔════╝
// ███████╗█████╗  ██║  ██║██║███████╗
// ╚════██║██╔══╝  ██║  ██║██║╚════██║
// ███████║███████╗██████╔╝██║███████║
// ╚══════╝╚══════╝╚═════╝ ╚═╝╚══════╝
// Copyright (C) 2014 - 2023 Sedis AB
// SamordningsnummerTests.cs
// Visual Studio Project: SwedishIdentityNumbers.Tests
// Created: 2023-10-31
// Last touched: 2023-10-31

using Xunit;

namespace SwedishIdentityNumbers.Tests
{
    namespace SwedishIdentityNumbers.Tests
    {
        public class SamordningsnummerTests
        {
            [Theory]
            [InlineData("701063-2391", true)]
            [InlineData("7010632391", true)]
            [InlineData("701063+2391", true)]
            [InlineData("701063-2390", false)]
            [InlineData("7010632390", false)]
            [InlineData("701063+2390", false)]
            public void TryCreate_ValidatesCorrectly(string number, bool isValid)
            {
                var success = Samordningsnummer.TryCreate(number, out var result);

                Assert.Equal(isValid, success);
                if (isValid)
                {
                    Assert.NotNull(result);
                    Assert.Equal(number.Replace("-", "").Replace("+", ""), result!.Number);
                }
                else
                {
                    Assert.Null(result);
                }
            }

            [Theory]
            [InlineData("701063-2391")]
            [InlineData("7010632391")]
            [InlineData("701063+2391")]
            public void Constructor_CreatesInstance(string number)
            {
                var samordningsnummer = new Samordningsnummer(number);

                Assert.NotNull(samordningsnummer);
                Assert.Equal(number.Replace("-", "").Replace("+", ""), samordningsnummer.Number);
            }

            [Theory]
            [InlineData("701063-2390")]
            [InlineData("7010632390")]
            [InlineData("701063+2390")]
            public void Constructor_ThrowsFormatException(string number)
            {
                Assert.Throws<FormatException>(() => new Samordningsnummer(number));
            }
        }
    }
}