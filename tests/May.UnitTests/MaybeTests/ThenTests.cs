using FluentAssertions;
using May.UnitTests.TestUtils.MaybeUtils;

namespace May.UnitTests.MaybeTests;

public class ThenTests
{
    private const string SomeStringValue = "SomeStringValue";
    private static readonly Maybe<string> SomeStringValueMaybe = SomeStringValue;
    private static readonly Task<string> SomeStringValueTask = Task.FromResult(SomeStringValue);
    private static readonly Task<Maybe<string>> SomeStringValueTaskMaybe = Task.FromResult(SomeStringValueMaybe);
    
    private static readonly List<ValidationError>  ValidationErrors = [ValidationError.From()];

    private static readonly Maybe<string> Something = "Something";
    private static readonly Maybe<string> SomethingButError = Error.Invalid();
    private static readonly Maybe<string> SomethingButValidationErrors = ValidationErrors;
    
    private static readonly Task<Maybe<string>> SomethingTask = Task.FromResult(Something);
    private static readonly Task<Maybe<string>> SomethingTaskButError = Task.FromResult(SomethingButError);
    private static readonly Task<Maybe<string>> SomethingTaskButValidationErrors = Task.FromResult(SomethingButValidationErrors);


    [Fact]
    public void Then_WithTValue_ReturnsNewValue()
    {
        var result = Something.Then((value) => SomeStringValue);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(SomeStringValue);
    }
    
    [Fact]
    public void Then_WithTValue_ReturnsNewMaybe()
    {
        var result = Something.Then((value) => SomeStringValueMaybe);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(SomeStringValue);
    }
    
    
    [Fact]
    public void Then_WithError_ReturnsError()
    {
        var result = SomethingButError.Then((value) => SomeStringValue);

        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    
    
    [Fact]
    public void Then_WithError_ReturnsError_NotMaybe()
    {
        var result = SomethingButError.Then((value) => SomeStringValueMaybe);

        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    
    [Fact]
    public void Then_WithValidationError_ReturnsValidationErrors()
    {
        var result = SomethingButValidationErrors.Then((value) => SomeStringValue);

        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }
    
    [Fact]
    public void Then_WithValidationError_ReturnsValidationErrors_NotMaybe()
    {
        var result = SomethingButValidationErrors.Then((value) => SomeStringValueMaybe);

        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }

    [Fact]
    public void ThenDo_WithTValue_ExecutesAction()
    {
        var result = string.Empty;

        Something.ThenDo((value) =>
        {
            result = value;
        });

        result.Should().Be(Something.Value);
    }
    
    
    [Fact]
    public void ThenDo_WithError_NotExecutesAction()
    {
        var result = string.Empty;

        SomethingButError.ThenDo((value) =>
        {
            result = value;
        });

        result.Should().Be(string.Empty);
    }
    
    [Fact]
    public void ThenDo_WithValidationError_NotExecutesAction()
    {
        var result = string.Empty;

        SomethingButValidationErrors.ThenDo((value) =>
        {
            result = value;
        });

        result.Should().Be(string.Empty);
    }

    [Fact]
    public async void ThenAsync_WithTValue_ReturnsNewValue()
    {
        var result = await SomethingTask.ThenAsync((value) => SomeStringValueTask);
        
        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(await SomeStringValueTask);
    }
    
    
    [Fact]
    public async void ThenAsync_WithTValue_ReturnsNewValueMaybe()
    {
        var result = await SomethingTask.ThenAsync((value) => SomeStringValueTaskMaybe);
        
        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(await SomeStringValueTask);
    }
    
    [Fact]
    public async void ThenAsync_WithError_NotExecutesAction()
    {
        var result = await SomethingTaskButError.ThenAsync((value) => SomeStringValueTask);
        
        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    
    
    [Fact]
    public async void ThenAsync_WithError_NotExecutesAction_NotMaybe()
    {
        var result = await SomethingTaskButError.ThenAsync((value) => SomeStringValueTaskMaybe);
        
        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    
    
    [Fact]
    public async void ThenAsync_WithValidationErrors_NotExecutesAction()
    {
        var result = await SomethingTaskButValidationErrors.ThenAsync((value) => SomeStringValueTask);
        
        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }
    
    [Fact]
    public async void ThenAsync_WithValidationErrors_NotExecutesAction_NotMaybe()
    {
        var result = await SomethingTaskButValidationErrors.ThenAsync((value) => SomeStringValueTaskMaybe);
        
        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }

    [Fact]
    public async void ThenDoAsync_WithTValue_ExecutesAction()
    {
        var result = await SomethingTask.ThenAsync((_)=>SomeStringValueTask).ThenDoAsync((value) => SomeStringValueTask);
        
        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(await SomeStringValueTask);
    }
    
    [Fact]
    public async void ThenDoAsync_WithError_NotExecutesAction()
    {
        var result = await SomethingTaskButError.ThenAsync((_)=>SomeStringValueTask).ThenDoAsync((value) => SomeStringValueTask);
        
        result.IsValue.Should().BeFalse();
        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    
    
    [Fact]
    public async void ThenDoAsync_WithValidationError_NotExecutesAction()
    {
        var result = await SomethingTaskButValidationErrors.ThenAsync((_)=>SomeStringValueTask).ThenDoAsync((value) => SomeStringValueTask);
        
        result.IsValue.Should().BeFalse();
        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }
    
    
    [Fact]
    public async void ThenExtension_WithTValue_ReturnsNewValue()
    {
        var result = await SomethingTask.Then((value) => SomeStringValue);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(SomeStringValue);
    }
    [Fact]
    public async void ThenExtension_WithError_ReturnsError()
    {
        var result = await SomethingTaskButError.Then((value) => SomeStringValue);

        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    [Fact]
    public async void ThenExtension_WithValidationError_ReturnsValidationErrors()
    {
        var result = await SomethingTaskButValidationErrors.Then((value) => SomeStringValue);

        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }
    
    
    [Fact]
    public async void ThenExtension_WithTValue_ReturnsNewMaybe()
    {
        var result = await SomethingTask.Then((value) => SomeStringValueMaybe);

        result.IsValue.Should().BeTrue();
        result.Value.Should().Be(SomeStringValue);
    }
    
    [Fact]
    public async void ThenExtension_WithError_ReturnsError_NotMaybe()
    {
        var result = await SomethingTaskButError.Then((value) => SomeStringValueMaybe);

        result.IsError.Should().BeTrue();
        result.Error.Should().Be(Error.Invalid());
    }
    [Fact]
    public async void ThenExtension_WithValidationError_ReturnsValidationErrors_NotMaybe()
    {
        var result = await SomethingTaskButValidationErrors.Then((value) => SomeStringValueMaybe);

        result.IsInvalid.Should().BeTrue();
        result.ValidationErrors.Should().BeEquivalentTo(ValidationErrors);
    }
    
    
    
    [Fact]
    public async void ThenDoExtension_WithTValue_ExecutesAction()
    {
        var result = string.Empty;

        await SomethingTask.ThenDo((value) =>
        {
            result = value;
        });

        result.Should().Be(Something.Value);
    }
    
    
    [Fact]
    public async void ThenDoExtension_WithError_NotExecutesAction()
    {
        var result = string.Empty;

        await SomethingTaskButError.ThenDo((value) =>
        {
            result = value;
        });

        result.Should().Be(string.Empty);
    }
    
    [Fact]
    public async void ThenDoExtension_WithValidationError_NotExecutesAction()
    {
        var result = string.Empty;

        await SomethingTaskButValidationErrors.ThenDo((value) =>
        {
            result = value;
        });

        result.Should().Be(string.Empty);
    }
}