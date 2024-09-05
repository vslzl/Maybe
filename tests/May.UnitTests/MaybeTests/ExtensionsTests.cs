using FluentAssertions;

namespace May.UnitTests.MaybeTests;

public class ExtensionsTests
{
    [Fact]
    public void ToMaybe_TValue_ShouldInit()
    {
        var value = "value";
        var maybe = value.ToMaybe();
        
        maybe.IsValue.Should().BeTrue();
        maybe.Value.Should().Be(value);
    }
    
    [Fact]
    public void MaybeFactory_TValue_ShouldInit()
    {
        var value = "value";
        
        var maybe = MaybeFactory.From(value);
        
        maybe.IsValue.Should().BeTrue();
        maybe.Value.Should().Be(value);
    }
    
    [Fact]
    public void ToMaybe_Null_ShouldNotInit()
    {
        string value = default!;
        var act = () =>
        {
            var maybe = value.ToMaybe();
        };
        
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void ToMaybe_Error_ShouldInitError()
    {
        var value = Error.Invalid();
        var maybe = value.ToMaybe<string>();
        
        maybe.IsError.Should().BeTrue();
        maybe.Error.Should().Be(value);
    }
    
    
    [Fact]
    public void ToMaybe_ValidationErrors_ShouldInitValidationErrors()
    {
        List<ValidationError> value = [ValidationError.From()];
        var maybe = value.ToMaybe<string>();
        
        maybe.IsInvalid.Should().BeTrue();
        maybe.ValidationErrors.Should().BeEquivalentTo(value);
    }
    
    
    [Fact]
    public void ToMaybe_EmptyValidationErrors_ShouldThrow()
    {
        List<ValidationError> value = [];
        var act = () =>
        {
            var maybe = value.ToMaybe<string>();
        };
        
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void ToMaybe_ValidationErrorsArray_ShouldInitValidationErrors()
    {
        ValidationError[] value = [ValidationError.From()];
        var maybe = value.ToMaybe<string>();
        
        maybe.IsInvalid.Should().BeTrue();
        maybe.ValidationErrors.Should().BeEquivalentTo(value);
    }
    
    [Fact]
    public void ToMaybe_EmptyValidationErrorsArray_ShouldThrow()
    {
        ValidationError[] value = [];
        var act = () =>
        {
            var maybe = value.ToMaybe<string>();
        };
        
        act.Should().Throw<InvalidOperationException>();
    }
}