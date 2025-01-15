using May.UnitTests.TestConstants;
using May.UnitTests.TestObjects;

namespace May.UnitTests.TestUtils.MaybeUtils;

public static class TestRecordFactory
{
    public static TestRecord Create(DateTime? dateTime = null, decimal dec = Constants.TestRecord.Decimal)
    {
        return new TestRecord(dateTime??Constants.TestRecord.DateTime, dec);
    }
}