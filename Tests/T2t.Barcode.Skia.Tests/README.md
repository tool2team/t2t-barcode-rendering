# T2t.Barcode.Skia.Tests

Unit test project for the T2t.Barcode.Skia library.

## Technologies

- **Test Framework**: xUnit
- **Code Coverage**: Coverlet
- **Target Frameworks**: .NET 7.0, .NET 8.0, .NET 10.0
- **Rendering Engine**: SkiaSharp

## Structure

```
Tests/T2t.Barcode.Skia.Tests/
├── T2t.Barcode.Skia.Tests.csproj
├── Usings.cs                      # Global usings
├── BarcodeDrawFactoryTests.cs     # Tests for BarcodeDrawFactory
├── Code39Tests.cs                 # Tests for Code39 barcode
├── Code128Tests.cs                # Tests for Code128 barcode
├── CodeQrTests.cs                 # Tests for QR Code
├── CodeEan13Tests.cs              # Tests for EAN-13 barcode
├── CodeDmTests.cs                 # Tests for DataMatrix barcode
└── BarcodeMetrics1dTests.cs       # Tests for BarcodeMetrics1d
```

## Test Coverage

### Barcode Types Tested
- ✅ Code39 (with and without checksum)
- ✅ Code128 (with checksum)
- ✅ QR Code
- ✅ EAN-13
- ✅ DataMatrix
- 🔲 EAN-8 (to be added)
- 🔲 Code93 (to be added)
- 🔲 Code11 (to be added)
- 🔲 Code25 variants (to be added)

### Test Scenarios
- ✅ Valid input generates SKBitmap
- ✅ Different input types
- ✅ Custom sizes
- ✅ Error handling (empty/invalid input)
- ✅ Unicode support (QR Code)
- ✅ Image dimensions validation

## Running Tests

### Visual Studio
Use the Test Explorer (Ctrl+E, T)

### Command Line
```bash
dotnet test
```

### With Code Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Run Specific Test Class
```bash
dotnet test --filter "FullyQualifiedName~CodeQrTests"
```

## Test Examples

### Basic Barcode Generation
```csharp
[Fact]
public void Code39_Draw_WithValidInput_ShouldGenerateImage()
{
    // Arrange
    var barcode = BarcodeDrawFactory.Code39WithoutChecksum;
    const string testData = "ABC123";

    // Act
    var result = barcode.Draw(testData);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Width > 0);
    Assert.True(result.Height > 0);
}
```

### Parameterized Tests
```csharp
[Theory]
[InlineData("12345")]
[InlineData("ABCDEF")]
[InlineData("Test123")]
public void Code128_Draw_WithDifferentInputs_ShouldGenerateImage(string input)
{
    // Arrange
    var barcode = BarcodeDrawFactory.Code128WithChecksum;

    // Act
    var result = barcode.Draw(input);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Width > 0);
}
```

## Conventions

- One test file per barcode type/class
- Naming: `{BarcodeType}Tests.cs`
- Use xUnit assertions: `Assert.NotNull()`, `Assert.Equal()`, `Assert.True()`, etc.
- AAA Pattern: Arrange, Act, Assert
- Test method naming: `{MethodUnderTest}_{Scenario}_{ExpectedResult}`
- Dispose SKBitmap instances when appropriate

## Notes

- SKBitmap instances should be disposed after use in production code
- Tests verify image dimensions and existence
- Some tests may throw `InvalidOperationException` for invalid input
- Custom size tests verify exact width and height of generated images

## TODO

- [ ] Add tests for remaining barcode types (EAN-8, Code93, Code11, Code25)
- [ ] Add tests for image pixel validation
- [ ] Add performance tests for large data sets
- [ ] Add tests for edge cases and boundary conditions
- [ ] Test different image formats and encodings
- [ ] Target code coverage > 80%
