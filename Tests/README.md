# T2t.Barcode Test Projects

This directory contains unit test projects for the T2t.Barcode rendering libraries.

## Test Projects

### 📦 T2t.Barcode.Svg.Tests
Unit tests for the SVG barcode renderer.
- **Target Frameworks**: .NET 8.0, .NET 10.0
- **Output Format**: SVG (Scalable Vector Graphics)
- **Platform**: Cross-platform
- **No Dependencies**: Pure .NET implementation

### 📦 T2t.Barcode.Skia.Tests
Unit tests for the SkiaSharp barcode renderer.
- **Target Frameworks**: .NET 8.0, .NET 10.0
- **Output Format**: SKBitmap (raster images)
- **Platform**: Cross-platform (Windows, Linux, macOS)
- **Dependencies**: SkiaSharp

### 📦 T2t.Barcode.Drawing.Tests
Unit tests for the System.Drawing barcode renderer.
- **Target Frameworks**: .NET 8.0-windows, .NET 10.0-windows
- **Output Format**: Bitmap (raster images)
- **Platform**: Windows only
- **Dependencies**: System.Drawing.Common

## Common Test Coverage

All test projects cover:
- ✅ **BarcodeDrawFactory** - Factory pattern and symbology mapping
- ✅ **Code39** - With and without checksum
- ✅ **Code128** - Various input types
- ✅ **QR Code** - URLs, Unicode, long text
- ✅ **EAN-13** - Valid and invalid barcodes
- ✅ **DataMatrix** - Various encodings
- ✅ **BarcodeMetrics1d** - Dimension handling

## Test Framework & Tools

- **Test Framework**: xUnit 2.9.3
- **Test Runner**: Visual Studio Test Explorer / dotnet test
- **Code Coverage**: Coverlet
- **Assertion Style**: Standard xUnit assertions (Assert.*)

## Platform-Specific Considerations

### T2t.Barcode.Drawing.Tests
- **Windows Only**: Requires Windows OS to run
- CI/CD pipelines must use Windows runners
- Cannot run on Linux or macOS

### T2t.Barcode.Svg.Tests & T2t.Barcode.Skia.Tests
- **Cross-platform**: Can run on Windows, Linux, and macOS
- No platform-specific dependencies

## Continuous Integration

All test projects are compatible with:
- ✅ GitHub Actions
- ✅ Azure Pipelines
- ✅ Jenkins
- ✅ TeamCity

## Code Coverage Goals

- **Target**: > 80% code coverage for each project
- **Current**: Tests cover main functionality, edge cases needed
- **Focus Areas**: 
  - Error handling
  - Boundary conditions
  - Invalid input validation

## Contributing

When adding new tests:
1. Follow existing naming conventions
2. Use AAA pattern (Arrange, Act, Assert)
3. Add tests for both happy path and error cases
4. Update relevant README files
5. Ensure all tests pass before committing

## TODO

- [ ] Add tests for remaining barcode types (EAN-8, Code93, Code11, Code25)
- [ ] Add integration tests
- [ ] Add performance benchmarks
- [ ] Increase code coverage to > 80%
- [ ] Add mutation testing
- [ ] Add visual regression tests for barcodes
