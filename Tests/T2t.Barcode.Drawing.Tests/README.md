# T2t.Barcode.Drawing.Tests

Unit test project for the T2t.Barcode.Drawing library.

## Technologies

- **Test Framework**: xUnit
- **Code Coverage**: Coverlet
- **Target Frameworks**: .NET 8.0-windows, .NET 10.0-windows
- **Rendering Engine**: System.Drawing.Common

## Structure

```
Tests/T2t.Barcode.Drawing.Tests/
├── T2t.Barcode.Drawing.Tests.csproj
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
- ✅ Valid input generates Bitmap
- ✅ Different input types
- ✅ Custom sizes
- ✅ Error handling (empty/invalid input)
- ✅ Unicode support (QR Code)
- ✅ Image dimensions validation

## Conventions

- One test file per barcode type/class
- Naming: `{BarcodeType}Tests.cs`
- Use xUnit assertions: `Assert.NotNull()`, `Assert.Equal()`, `Assert.True()`, etc.
- AAA Pattern: Arrange, Act, Assert
- Test method naming: `{MethodUnderTest}_{Scenario}_{ExpectedResult}`
- Dispose Bitmap instances when appropriate

## Platform Requirements

- **Windows Only**: This library depends on System.Drawing.Common which requires Windows
- Tests must run on Windows platform
- CI/CD pipelines must use Windows runners

## Notes

- Bitmap instances should be disposed after use in production code
- Tests verify image dimensions and existence
- Some tests may throw `InvalidOperationException` for invalid input
- Custom size tests verify exact width and height of generated images
- GDI+ is used under the hood for rendering

## TODO

- [ ] Add tests for remaining barcode types (EAN-8, Code93, Code11, Code25)
- [ ] Add tests for image pixel validation
- [ ] Add performance tests for large data sets
- [ ] Add tests for edge cases and boundary conditions
- [ ] Test different image formats (PNG, BMP, JPEG)
- [ ] Test color customization
- [ ] Target code coverage > 80%
