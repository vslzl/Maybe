namespace May;

public readonly partial record struct Maybe<TValue>
{
    public bool Equals(Maybe<TValue> other)
    {
        if (IsInvalid) return other.IsInvalid && CheckIfValidationErrorsAreEqual(_validationErrors, other._validationErrors);
        if (IsError) return other.IsError && _error.Equals(other._error);
        return other.IsValue && EqualityComparer<TValue>.Default.Equals(_value, other._value);
    }

    public override int GetHashCode()
    {
        if (IsValue)
        {
            return _value.GetHashCode();
        }
        
        if (IsError)
        {
            return _error.GetHashCode();
        }
        
        var hashCode = new HashCode();
        
        for (var i = 0; i < _validationErrors.Count; i++)
        {
            hashCode.Add(_validationErrors[i]);
        }

        return hashCode.ToHashCode();
    }

    private static bool CheckIfValidationErrorsAreEqual(List<ValidationError> errors1, List<ValidationError> errors2)
    {
        if (ReferenceEquals(errors1, errors2))
        {
            return true;
        }

        if (errors1.Count != errors2.Count)
        {
            return false;
        }

        for (var i = 0; i < errors1.Count; i++)
        {
            if (!errors1[i].Equals(errors2[i]))
            {
                return false;
            }
        }

        return true;
    }
}