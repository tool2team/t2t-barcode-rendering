# T2t.Barcode.Svg.Tests

Unit test project for the T2t.Barcode.Svg library.

## Technologies

- **Test Framework**: xUnit
- **Code Coverage**: Coverlet
- **Target Frameworks**: .NET 8.0, .NET 10.0

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
- ✅ Valid input generates SVG
- ✅ Different input types
- ✅ Custom sizes
- ✅ Error handling (empty/invalid input)
- ✅ Unicode support (QR Code)

## Conventions

- One test file per barcode type/class
- Naming: `{BarcodeType}Tests.cs`
- Use xUnit assertions: `Assert.NotNull()`, `Assert.Equal()`, `Assert.Contains()`, etc.
- AAA Pattern: Arrange, Act, Assert
- Test method naming: `{MethodUnderTest}_{Scenario}_{ExpectedResult}`

## TODO

- [ ] Add tests for remaining barcode types (EAN-8, Code93, Code11, Code25)
- [ ] Add integration tests for SVG output validation
- [ ] Add performance tests for large data sets
- [ ] Add tests for edge cases and boundary conditions
- [ ] Validate SVG structure and attributes
- [ ] Test color customization options
- [ ] Target code coverage > 80%

## Notes

- Some tests may throw `InvalidOperationException` for invalid input - verify actual exception types from the library
- SVG output validation focuses on structure; visual validation should be done manually or with additional tools
- Custom size tests verify dimension attributes in SVG output
