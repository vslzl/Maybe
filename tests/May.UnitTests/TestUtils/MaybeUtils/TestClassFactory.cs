using May.UnitTests.TestConstants;
using May.UnitTests.TestObjects;

namespace May.UnitTests.TestUtils.MaybeUtils;

public static class TestClassFactory
{
    public static TestClass Create(string value = Constants.TestClass.Value, Guid? id = null)
    {
        return new TestClass(value, id);
    }
}