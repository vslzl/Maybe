namespace May;

[Flags]
public enum State : byte
{
    Value = 0b0001,
    Error = 0b0010,
    Invalid = 0b0100,
}