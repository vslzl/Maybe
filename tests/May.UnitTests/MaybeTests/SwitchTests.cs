using FluentAssertions;

namespace May.UnitTests.MaybeTests;

public class SwitchTests
{
    private static readonly Maybe<string> Something = "Something";
    private static readonly Maybe<string> SomethingButError = Error.Invalid();
    private static readonly Maybe<string> SomethingButInvalid = (List<ValidationError>) [ValidationError.From()];

    private static readonly Task<Maybe<string>> SomethingTask = Task.FromResult(Something);
    private static readonly Task<Maybe<string>> SomethingButErrorTask = Task.FromResult(SomethingButError);
    private static readonly Task<Maybe<string>> SomethingButInvalidTask = Task.FromResult(SomethingButInvalid);

    [Fact]
    public void Switch_WhenTValue_ShouldRunOnValue()
    {
        var result = string.Empty;
        Something.Switch((value) => result = value.ToUpper(), (err) => result = err.Code.ToUpper(),
            (validationErrors) => result = "INVALID");

        result.Should().Be(Something.Value.ToUpper());
    }
    
    
    [Fact]
    public void Switch_WhenError_ShouldRunOnError()
    {
        var result = string.Empty;
        
        SomethingButError.Switch((value) => result = value.ToUpper(), (err) => result = err.Code.ToUpper(),
            (validationErrors) => result = "INVALID");

        result.Should().Be(SomethingButError.Error.Code.ToUpper());
    }
    
    [Fact]
    public void Switch_WhenValidationErrors_ShouldRunOnInvalid()
    {
        var result = string.Empty;
        SomethingButInvalid.Switch((value) => result = value.ToUpper(), (err) => result = err.Code.ToUpper(),
            (validationErrors) => result = "INVALID");

        result.Should().Be("INVALID");
    }

    [Fact]
    public async void SwitchAsync_WhenTValue_ShouldRunOnValue()
    {
        var result = string.Empty;

        await SomethingTask.SwitchAsync(
            (value) => Task.Run(() => result = value.ToUpper()),
            (err) => Task.Run(() => result = err.Code.ToUpper()),
            (validationErrors) => Task.Run(() => result = "INVALID")
            );
        
        result.Should().Be((await SomethingTask).Value.ToUpper());
    }
    
    
    [Fact]
    public async void SwitchAsync_WhenError_ShouldRunOnError()
    {
        var result = string.Empty;

        await SomethingButErrorTask.SwitchAsync(
            (value) => Task.Run(() => result = value.ToUpper()),
            (err) => Task.Run(() => result = err.Code.ToUpper()),
            (validationErrors) => Task.Run(() => result = "INVALID")
            );
        
        result.Should().Be((await SomethingButErrorTask).Error.Code.ToUpper());
    }
    
    
    [Fact]
    public async void SwitchAsync_WhenValidationErrors_ShouldRunOnInvalid()
    {
        var result = string.Empty;

        await SomethingButInvalidTask.SwitchAsync(
            (value) => Task.Run(() => result = value.ToUpper()),
            (err) => Task.Run(() => result = err.Code.ToUpper()),
            (validationErrors) => Task.Run(() => result = "INVALID")
            );
        
        result.Should().Be("INVALID");
    }
    
    
    [Fact]
    public async void SwitchExtension_WhenTValue_ShouldRunOnValue()
    {
        var result = string.Empty;
        await SomethingTask.Switch((value) => result = value.ToUpper(), (err) => result = err.Code.ToUpper(),
            (validationErrors) => result = "INVALID");

        result.Should().Be(Something.Value.ToUpper());
    }
    
    
    [Fact]
    public async void SwitchExtension_WhenError_ShouldRunOnError()
    {
        var result = string.Empty;
        
        await SomethingButErrorTask.Switch((value) => result = value.ToUpper(), (err) => result = err.Code.ToUpper(),
            (validationErrors) => result = "INVALID");

        result.Should().Be(SomethingButError.Error.Code.ToUpper());
    }
    
    [Fact]
    public async void SwitchExtension_WhenValidationErrors_ShouldRunOnInvalid()
    {
        var result = string.Empty;
        await SomethingButInvalidTask.Switch((value) => result = value.ToUpper(), (err) => result = err.Code.ToUpper(),
            (validationErrors) => result = "INVALID");

        result.Should().Be("INVALID");
    }
}