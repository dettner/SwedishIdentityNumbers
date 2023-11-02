Based on the provided class definitions and their methods, here is an updated version of your README.md file:

# SwedishIdentityNumbers

A .NET library for working with Swedish identity numbers.

## Overview

The `SwedishIdentityNumbers` library provides a set of classes for working with Swedish identity numbers:
- `Personnummer` for personal identity numbers
- `Samordningsnummer` for coordination numbers
- `Organisationsnummer` for organization numbers

Each of these classes inherits from the abstract base class `SwedishIdentityNumber`, which provides common functionality for validating and working with identity numbers.

## Installation

Install `SwedishIdentityNumbers` via NuGet:

```bash
dotnet add package SwedishIdentityNumbers
```

Or search for `SwedishIdentityNumbers` in the NuGet package manager in Visual Studio.

## Usage

### Create a `Personnummer`

```csharp
using SwedishIdentityNumbers;

var personnummer = new Personnummer("8507301234"); // also allows 850730-1234
```

#### Get the Date of Birth (a DateOnly) from a `Personnummer`

```csharp
var dateOfBirth = personnummer.DateOfBirth;
```

### Try to create a `Personnummer`

```csharp
using SwedishIdentityNumbers;

if (Personnummer.TryCreate("8507301234", out var personnummer)) // also allows 850730-1234
{
    // Successfully created
}
```

### Create a `Samordningsnummer`

```csharp
using SwedishIdentityNumbers;

var samordningsnummer = new Samordningsnummer("8507901234"); // also allows 850700-1234
```

#### Get the Date of Birth (a DateOnly) from a `Samordningsnummer`

```csharp
var dateOfBirth = samordningsnummer.DateOfBirth;
```

### Working with `Organisationsnummer`

```csharp
using SwedishIdentityNumbers;

var organisationsnummer = new Organisationsnummer("5560360793");
```

#### Determine the Probable Swedish Company Form

```csharp
var companyForm = organisationsnummer.ProbableSwedishCompanyForm;
```

## Development

To work on `SwedishIdentityNumbers`, clone the repo and open `SwedishIdentityNumbers.sln` in Visual Studio or your preferred editor.

## Testing

Run the tests in `SwedishIdentityNumbers.Tests` to ensure everything is working correctly.

## License

`SwedishIdentityNumbers` is licensed under the MIT License. See the [LICENSE](./LICENSE.txt) file for details.
