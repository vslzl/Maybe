using May.UnitTests.TestConstants;

namespace May.UnitTests.TestObjects;

public class TestClass : IEquatable<TestClass>
{
    public TestClass(string value, Guid? id = null)
    {
        Id = id ?? Constants.TestClass.Id;
        Value = value;
    }

    public Guid Id { get; set; }
    public string Value { get; set; }

    public bool Equals(TestClass? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TestClass)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Value);
    }
}