using FluentAssertions;
using May.UnitTests.TestObjects;

namespace May.UnitTests;

public class Tests
{
    [Fact]
    public void FlagTests()
    {
        var error = Error.Invalid();
        var maybeFromError = Maybe<TestClass>.FromError(error);
        
        var result = maybeFromError.Is(State.Invalid | State.Error);
        result.Should().BeTrue();
        
        result = maybeFromError.Is(State.Error);
        result.Should().BeTrue();
        
        result = maybeFromError.Is(State.Invalid);
        result.Should().BeFalse();
        
        result = maybeFromError.Is(State.Value);
        result.Should().BeFalse();
        
        result = maybeFromError.Is(State.Value | State.Invalid);
        result.Should().BeFalse();
        
        result = maybeFromError.Is(State.Value | State.Error);
        result.Should().BeTrue();
    }
}