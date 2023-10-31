
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

var personnummer = new Personnummer("198507301234");
```

### Try to create a `Personnummer`

```csharp
using SwedishIdentityNumbers;

if (Personnummer.TryCreate("198507301234", out var personnummer))
{
    // Successfully created
}
```

### Create a `Samordningsnummer`

```csharp
using SwedishIdentityNumbers;

var samordningsnummer = new Samordningsnummer("198507301234");
```

### Validate an identity number

```csharp
using SwedishIdentityNumbers;

var isValid = new Personnummer("198507301234").IsValidFormat();
```

## Development

To work on `SwedishIdentityNumbers`, clone the repo and open `SwedishIdentityNumbers.sln` in Visual Studio or your preferred editor.

## Testing

Run the tests in `SwedishIdentityNumbers.Tests` to ensure everything is working correctly.

## License

`SwedishIdentityNumbers` is licensed under the MIT License. See the [LICENSE](./LICENSE.txt) file for details.
