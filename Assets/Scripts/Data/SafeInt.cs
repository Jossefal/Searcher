public struct SafeInt
{
    private int _value;
    private int _offset;

    public SafeInt(int value)
    {
        _offset = UnityEngine.Random.Range(1000, 10000);
        _value = value + _offset;
    }

    public int GetValue()
    {
        return _value - _offset;
    }

    public static SafeInt operator +(SafeInt op1, SafeInt op2)
    {
        return new SafeInt(op1.GetValue() + op2.GetValue());
    }

    public static SafeInt operator -(SafeInt op1, SafeInt op2)
    {
        return new SafeInt(op1.GetValue() - op2.GetValue());
    }

    public static SafeInt operator ++(SafeInt op1)
    {
        return new SafeInt(op1.GetValue() + 1);
    }

    public static SafeInt operator --(SafeInt op1)
    {
        return new SafeInt(op1.GetValue() + 1);
    }

    public static bool operator >(SafeInt op1, SafeInt op2)
    {
        return op1.GetValue() > op2.GetValue();
    }

    public static bool operator <(SafeInt op1, SafeInt op2)
    {
        return op1.GetValue() < op2.GetValue();
    }
}
