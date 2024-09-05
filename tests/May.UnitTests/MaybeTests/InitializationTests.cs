using FluentAssertions;
using May.UnitTests.TestObjects;
using May.UnitTests.TestUtils.MaybeUtils;

namespace May.UnitTests.MaybeTests;

public class InitializationTests
{
    [Fact]
    public void Create_FromTValue_ShouldAssignValue()
    {
        var testClass = TestClassFactory.Create();
        var testRecord = TestRecordFactory.Create();
        var testStruct = TestStructFactory.Create();

        Maybe<TestClass> maybeTestClass = testClass;
        Maybe<TestRecord> maybeTestRecord = testRecord;
        Maybe<TestStruct> maybeTestStruct = testStruct;

        maybeTestClass.IsValue.Should().BeTrue();
        maybeTestClass.Value.Should().Be(testClass);
        maybeTestClass.IsValid.Should().BeTrue();
        maybeTestClass.Is(State.Value).Should().BeTrue();

        maybeTestRecord.IsValue.Should().BeTrue();
        maybeTestRecord.Value.Should().Be(testRecord);
        maybeTestRecord.IsValid.Should().BeTrue();
        maybeTestRecord.Is(State.Value).Should().BeTrue();

        maybeTestStruct.IsValue.Should().BeTrue();
        maybeTestStruct.Value.Should().Be(testStruct);
        maybeTestStruct.IsValid.Should().BeTrue();
        maybeTestStruct.Is(State.Value).Should().BeTrue();
    }

    [Fact]
    public void Create_FromNullValues_ShouldThrow()
    {
        TestClass testClass = default!;
        Error error = default;

        var act = () =>
        {
            Maybe<TestClass> maybeTestClass = testClass;
        };

        var actDefault = () =>
        {
            Maybe<TestClass> maybeTestClass = new Maybe<TestClass>();
        };

        var actError = () =>
        {
            Maybe<TestClass> maybeTestClass = error;
        };

        var actValidationErrors = () =>
        {
            Maybe<TestClass> maybeTestClass = new List<ValidationError>();
        };

        act.Should().ThrowExactly<InvalidOperationException>();
        actDefault.Should().ThrowExactly<InvalidOperationException>();
        actError.Should().ThrowExactly<InvalidOperationException>();
        actValidationErrors.Should().ThrowExactly<InvalidOperationException>();
    }

    [Fact]
    public void Create_FromError_ShouldAssignError()
    {
        var errorInvalid = Error.Invalid();
        var errorForbidden = Error.Forbidden();

        List<ValidationError> validationErrors = [ValidationError.From()];

        Maybe<TestClass> maybeInvalid = errorInvalid;
        Maybe<TestClass> maybeForbidden = errorForbidden;
        Maybe<TestClass> maybeValidationErrors = validationErrors;

        maybeInvalid.IsError.Should().BeTrue();
        maybeInvalid.Error.Should().Be(errorInvalid);

        maybeForbidden.IsError.Should().BeTrue();
        maybeForbidden.Error.Should().Be(errorForbidden);

        maybeValidationErrors.IsInvalid.Should().BeTrue();
        maybeValidationErrors.IsValid.Should().BeFalse();
        maybeValidationErrors.ValidationErrors.Should().BeEquivalentTo(validationErrors);
    }


    [Fact]
    public void Create_TValue_FromMaybe_ShouldAssignValue()
    {
        Maybe<TestClass> maybe = TestClassFactory.Create();

        TestClass testClass = maybe;

        testClass.Should().NotBeNull();
        testClass.Should().Be(maybe.Value);
    }

    [Fact]
    public void Create_from_Defaults()
    {
        Maybe<Success> success = Results.Success;
        Maybe<Created> created = Results.Created;
        Maybe<Updated> updated = Results.Updated;
        Maybe<Deleted> deleted = Results.Deleted;
        
        success.IsValid.Should().BeTrue();
        success.Value.Should().Be(Results.Success);
        
        created.IsValid.Should().BeTrue();
        created.Value.Should().Be(Results.Created);
        
        updated.IsValid.Should().BeTrue();
        updated.Value.Should().Be(Results.Updated);
        
        deleted.IsValid.Should().BeTrue();
        deleted.Value.Should().Be(Results.Deleted);
    }
}