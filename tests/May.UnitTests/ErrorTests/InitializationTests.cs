using FluentAssertions;
using FluentAssertions.Execution;

namespace May.UnitTests.ErrorTests;

public class InitializationTests
{
    public static Dictionary<string, object> DefaultMeta = new Dictionary<string, object>() { { "foo", "bar" } };

    public static Dictionary<string, object> OtherMeta = new Dictionary<string, object>()
        { { "foo", "baz" }, { "bar", "qux" } };

    public static TheoryData<Error, Error> SimilarErrors = new()
    {
        { Error.Invalid(), Error.Invalid() },
        { Error.Unauthorized(), Error.Unauthorized() },
        { Error.Forbidden(), Error.Forbidden() },
        { Error.NotFound(), Error.NotFound() },
        { Error.Conflict(), Error.Conflict() },
        { Error.Failure(), Error.Failure() },
        { Error.Unavailable(), Error.Unavailable() },
        { Error.Critical(), Error.Critical() },
        { Error.Custom(1,"",""), Error.Custom(1,"","") },
        { Error.Critical(metadata: DefaultMeta), Error.Critical(metadata: DefaultMeta) },
    };

    public static TheoryData<Dictionary<string, object>, Dictionary<string, object>> DifferentMetadata = new()
    {
        //different count, same items
        {
            new Dictionary<string, object>() { { "foo", "bar" }, { "foz", "baz" } },
            new Dictionary<string, object>() { { "foo", "bar" }, { "foz", "baz" }, { "for", "bay" } }
        },
        //different count, different items
        {
            new Dictionary<string, object>() { { "foo", "bar" }, { "foz", "baz" } },
            new Dictionary<string, object>() { { "fiz", "biz" }, { "oz", "az" }, { "or", "ay" } }
        },
        //same count, different items
        {
            new Dictionary<string, object>() { { "foo", "bar" }, { "foz", "baz" } },
            new Dictionary<string, object>() { { "fiz", "biz" }, { "oz", "az" } }
        },
    };

    public static TheoryData<Dictionary<string, object>?, Dictionary<string, object>?> SimilarMetadata = new()
    {
        //same count, same items
        {
            new Dictionary<string, object>() { { "foo", "bar" }, { "foz", "baz" } },
            new Dictionary<string, object>() { { "foo", "bar" }, { "foz", "baz" } }
        },
        //null metadata
        {
            null,
            null
        },
        //empty metadata
        {
            new Dictionary<string, object>() { },
            new Dictionary<string, object>() { }
        },
    };

    public static TheoryData<Error, Error> DifferentErrors = new()
    {
        { Error.Invalid(), Error.Unauthorized() },
        { Error.Unauthorized(), Error.Forbidden() },
        { Error.Forbidden(), Error.Invalid() },
        { Error.Invalid(metadata: DefaultMeta), Error.Invalid(metadata: OtherMeta) },
    };

    [Theory]
    [MemberData(nameof(SimilarErrors))]
    public void Equality_SimilarErrors_ShouldBeTrue(Error error, Error other)
    {
        var result = error.Equals(other);
        result.Should().BeTrue();
    }
    
    [Theory]
    [MemberData(nameof(DifferentErrors))]
    public void Equality_DifferentErrors_ShouldBeFalse(Error error, Error other)
    {
        var result = error.Equals(other);
        result.Should().BeFalse();
    }
    
    [Theory]
    [MemberData(nameof(SimilarErrors))]
    public void GetHashCode_SimilarErrors_ShouldBeTrue(Error error, Error other)
    {
        var errorHashCode = error.GetHashCode();
        var otherHashCode = other.GetHashCode();
        
        errorHashCode.Should().Be(otherHashCode);
    }
    
    [Theory]
    [MemberData(nameof(DifferentErrors))]
    public void GetHashCode_DifferentErrors_ShouldBeFalse(Error error, Error other)
    {
        
        var errorHashCode = error.GetHashCode();
        var otherHashCode = other.GetHashCode();
        
        errorHashCode.Should().NotBe(otherHashCode);
    }

    [Theory]
    [MemberData(nameof(DifferentMetadata))]
    public void Equality_SameErrors_WithDifferentMeta_ShouldBeFalse(Dictionary<string, object> error,
        Dictionary<string, object> other)
    {
        var e1 = Error.Invalid(metadata: error);
        var e2 = Error.Invalid(metadata: other);
        
        
        var result = e1.Equals(e2);
        
        result.Should().BeFalse();
    }
    
    
    [Theory]
    [MemberData(nameof(DifferentMetadata))]
    public void GetHashCode_SameErrors_WithDifferentMeta_ShouldBeFalse(Dictionary<string, object> error,
        Dictionary<string, object> other)
    {
        var e1 = Error.Invalid(metadata: error);
        var e2 = Error.Invalid(metadata: other);
        
        
        var errorHashCode = e1.GetHashCode();
        var otherHashCode = e2.GetHashCode();
        
        errorHashCode.Should().NotBe(otherHashCode);
    }


    [Theory]
    [MemberData(nameof(SimilarMetadata))]
    public void Equality_SameErrors_SameDifferentMeta_ShouldBeTrue(Dictionary<string, object> error,
        Dictionary<string, object> other)
    {
        var e1 = Error.Invalid(metadata: error);
        var e2 = Error.Invalid(metadata: other);
        
        var result = e1.Equals(e2);

        result.Should().BeTrue();
    }
    
    [Theory]
    [MemberData(nameof(SimilarMetadata))]
    public void GetHashCode_SameErrors_SameDifferentMeta_ShouldBeTrue(Dictionary<string, object> error,
        Dictionary<string, object> other)
    {
        var e1 = Error.Invalid(metadata: error);
        var e2 = Error.Invalid(metadata: other);
        
        var errorHashCode = e1.GetHashCode();
        var otherHashCode = e2.GetHashCode();
        
        errorHashCode.Should().Be(otherHashCode);
    }
}