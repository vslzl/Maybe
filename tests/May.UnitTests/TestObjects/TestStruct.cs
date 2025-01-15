namespace May.UnitTests.TestObjects;

public struct TestStruct
{
    public TestStruct(int value)
    {
        Value = value;
    }

    public int Value { get; set; }
}