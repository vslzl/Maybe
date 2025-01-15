using FluentAssertions;
using May.UnitTests.TestObjects;

namespace May.UnitTests.MaybeTests;

public class EqualityTests
{
    public static TheoryData<Maybe<int>, Maybe<int>> ShouldNotBeEqual =>
        new TheoryData<Maybe<int>, Maybe<int>>()
        {
            // value but not equal
            {
                3,
                4
            },
            // one value, one error
            {
                Error.Invalid(),
                4
            },
            // one value, one validation errors
            {
                (List<ValidationError>) [ValidationError.From()],
                4
            },
            // one error, one validation errors
            {
                (List<ValidationError>) [ValidationError.From()],
                Error.Invalid()
            },
        };

    public static TheoryData<Maybe<int>, Maybe<int>> ShouldBeEqual =>
        new TheoryData<Maybe<int>, Maybe<int>>()
        {
            { 1, 1 },
            { Error.Invalid(), Error.Invalid() },
            { Error.Forbidden(), Error.Forbidden() },
            {
                (List<ValidationError>) [ValidationError.From(), ValidationError.From()],
                (List<ValidationError>) [ValidationError.From(), ValidationError.From()]
            }
        };

    private static readonly List<ValidationError> DefaultValidationErrors = [ValidationError.From(), ValidationError.From()];

    public static TheoryData<List<ValidationError>, List<ValidationError>> DifferentValidationErrors =>
        new()
        {
            // same errors but different count
            {
                [..DefaultValidationErrors, ValidationError.From()],
                [..DefaultValidationErrors]
            },
            // same count but different errors
            {
                [..DefaultValidationErrors, ValidationError.From()],
                [..DefaultValidationErrors, ValidationError.From("Custom.Code", "Custom Message")]
            }
        };

    public static TheoryData<List<ValidationError>, List<ValidationError>> SimilarValidationErrors =>
        new()
        {
            // same errors but different references
            {
                [..DefaultValidationErrors],
                [..DefaultValidationErrors]
            },
            // same errors with same references
            {
                DefaultValidationErrors,
                DefaultValidationErrors
            }
        };

    [Theory]
    [MemberData(nameof(ShouldNotBeEqual))]
    public void Equality_NotEqual_ShouldBeNotEqual(Maybe<int> maybeLeft, Maybe<int> maybeRight)
    {
        var result = maybeLeft.Equals(maybeRight);

        result.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(ShouldBeEqual))]
    public void Equality_WhenEqual_ShouldBeEqual(Maybe<int> maybeLeft, Maybe<int> maybeRight)
    {
        var result = maybeLeft.Equals(maybeRight);

        result.Should().BeTrue();
    }
    
    [Theory]
    [MemberData(nameof(SimilarValidationErrors))]
    public void Equality_WhenSimilarValidationErrors_ShouldBeEqual(List<ValidationError> left, List<ValidationError> right)
    {
        Maybe<TestClass> maybeLeft = left;
        Maybe<TestClass> maybeRight = right;

        var result = maybeLeft.Equals(maybeRight);
        
        result.Should().BeTrue();
    }   
    
    
    [Theory]
    [MemberData(nameof(DifferentValidationErrors))]
    public void Equality_WhenDifferentValidationErrors_ShouldNotBeEqual(List<ValidationError> left, List<ValidationError> right)
    {
        Maybe<TestClass> maybeLeft = left;
        Maybe<TestClass> maybeRight = right;

        var result = maybeLeft.Equals(maybeRight);
        
        result.Should().BeFalse();
    }


    [Theory]
    [MemberData(nameof(ShouldNotBeEqual))]
    public void GetHashCode_NotEqual_ShouldBeNotEqual(Maybe<int> maybeLeft, Maybe<int> maybeRight)
    {
        var hashCodeLeft = maybeLeft.GetHashCode();
        var hashCodeRight = maybeRight.GetHashCode();

        hashCodeLeft.Should().NotBe(hashCodeRight);
    }

    [Theory]
    [MemberData(nameof(ShouldBeEqual))]
    public void GetHashCode_WhenEqual_ShouldBeEqual(Maybe<int> maybeLeft, Maybe<int> maybeRight)
    {
        var hashCodeLeft = maybeLeft.GetHashCode();
        var hashCodeRight = maybeRight.GetHashCode();

        hashCodeLeft.Should().Be(hashCodeRight);
    }

    [Theory]
    [MemberData(nameof(DifferentValidationErrors))]
    public void GetHashCode_NotEqual_WhenDifferentValidationErrorsOccured(List<ValidationError> left,
        List<ValidationError> right)
    {
        Maybe<TestClass> maybeLeft = left;
        Maybe<TestClass> maybeRight = right;

        var hashCodeLeft = maybeLeft.GetHashCode();
        var hashCodeRight = maybeRight.GetHashCode();

        hashCodeLeft.Should().NotBe(hashCodeRight);
    }

    [Theory]
    [MemberData(nameof(SimilarValidationErrors))]
    public void GetHashCode_NotEqual_WhenSimilarValidationErrorsOccured(List<ValidationError> left,
        List<ValidationError> right)
    {
        Maybe<TestClass> maybeLeft = left;
        Maybe<TestClass> maybeRight = right;

        var hashCodeLeft = maybeLeft.GetHashCode();
        var hashCodeRight = maybeRight.GetHashCode();

        hashCodeLeft.Should().Be(hashCodeRight);
    }
}