public struct SafeInt
{
    private int value;
    private int offset;

    public SafeInt(int value)
    {
        this.offset = UnityEngine.Random.Range(1000, 10000);
        this.value = value + offset;
    }

    public int GetValue()
    {
        return value - offset;
    }

    public override string ToString()
    {
        return Converter.ConvertToString(GetValue());
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
