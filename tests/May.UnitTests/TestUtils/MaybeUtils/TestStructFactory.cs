using May.UnitTests.TestConstants;
using May.UnitTests.TestObjects;

namespace May.UnitTests.TestUtils.MaybeUtils;

public static class TestStructFactory
{
    public static TestStruct Create(int value = Constants.TestStruct.Value)
    {
        return new TestStruct(value);
    }
}