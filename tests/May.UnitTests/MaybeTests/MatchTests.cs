using FluentAssertions;

namespace May.UnitTests.MaybeTests;

public class MatchTests
{
    private static readonly Maybe<string> Something = "Something";
    private static readonly Maybe<string> SomethingButError = Error.Invalid();
    private static readonly Maybe<string> SomethingButInvalid = (List<ValidationError>) [ValidationError.From()];
    
    private static readonly Task<Maybe<string>> SomethingTask = Task.FromResult(Something);
    private static readonly Task<Maybe<string>> SomethingButErrorTask = Task.FromResult(SomethingButError);
    private static readonly Task<Maybe<string>> SomethingButInvalidTask = Task.FromResult(SomethingButInvalid);

    [Fact]
    public void Match_WhenTValue_ShouldRunOnValue()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = Something.Match(_ => successValue, _ => errorValue, _ => validationErrorValue);

        result.Should().Be(successValue);
    }
    
    [Fact]
    public void Match_WhenError_ShouldRunOnError()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = SomethingButError.Match(_ => successValue, _ => errorValue, _ => validationErrorValue);

        result.Should().Be(errorValue);
    }
    
    [Fact]
    public void Match_WhenValidationError_ShouldRunOnInvalid()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = SomethingButInvalid.Match(_ => successValue, _ => errorValue, _ => validationErrorValue);

        result.Should().Be(validationErrorValue);
    }
    
    
    [Fact]
    public async void MatchAsync_WhenTValue_ShouldRunOnValue()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = await SomethingTask.MatchAsync(_ => Task.FromResult(successValue), _ => Task.FromResult(errorValue), _ => Task.FromResult(validationErrorValue));

        result.Should().Be(successValue);
    }
    
    [Fact]
    public async void MatchAsync_WhenError_ShouldRunOnError()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = await SomethingButErrorTask.MatchAsync(_ => Task.FromResult(successValue), _ => Task.FromResult(errorValue), _ => Task.FromResult(validationErrorValue));

        result.Should().Be(errorValue);
    }
    
    [Fact]
    public async void MatchAsync_WhenValidationError_ShouldRunOnInvalid()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = await SomethingButInvalidTask.MatchAsync(_ => Task.FromResult(successValue), _ => Task.FromResult(errorValue), _ => Task.FromResult(validationErrorValue));

        result.Should().Be(validationErrorValue);
    }

    [Fact]
    public async void MatchExtension_WhenTValue_ShouldRunOnValue()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";

        var result = await SomethingTask.Match(_ => successValue, _ => errorValue, _ => validationErrorValue);
        
        result.Should().Be(successValue);
    }
    
    
    [Fact]
    public async  void MatchExtension_WhenError_ShouldRunOnError()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = await SomethingButErrorTask.Match(_ => successValue, _ => errorValue, _ => validationErrorValue);

        result.Should().Be(errorValue);
    }
    
    [Fact]
    public  async void MatchExtension_WhenValidationError_ShouldRunOnInvalid()
    {
        var successValue = "Success";
        var errorValue = "Error";
        var validationErrorValue = "Invalid";
        var result = await SomethingButInvalidTask.Match(_ => successValue, _ => errorValue, _ => validationErrorValue);

        result.Should().Be(validationErrorValue);
    }
}