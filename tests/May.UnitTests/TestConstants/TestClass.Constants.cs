namespace May.UnitTests.TestConstants;

public static class Constants
{
    public static class TestClass
    {
        public static readonly Guid Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public const string Value = "Hello World";
    }

    public static class TestRecord
    {
        public static readonly DateTime DateTime = DateTime.Parse("2020-02-02 10:10:10");
        public const decimal Decimal = 10.00M;
    }

    public static class TestStruct
    {
        public const int Value = 42;
    }
    
    public const string StringValue = "Hello World";
}