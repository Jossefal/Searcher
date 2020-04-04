using System;
using System.Globalization;

public class Converter
{
    private static CultureInfo cultureInfo = new CultureInfo("en-US");

    public static string ConvertToString(int value)
    {
        return Convert.ToString(value, cultureInfo);
    }

    public static string ConvertToString(float value)
    {
        return Convert.ToString(value, cultureInfo);
    }

    public static string ConvertToString(double value)
    {
        return Convert.ToString(value, cultureInfo);
    }

    public static int ConvertToInt32(string value)
    {
        if (value == "")
            return 0;
        else
            return Convert.ToInt32(value, cultureInfo);
    }

    public static float ConvertToFloat(string value)
    {
        if (value == "")
            return 0f;
        else
            return Convert.ToSingle(value, cultureInfo);
    }

    public static double ConvertToDouble(string value)
    {
        if (value == "")
            return 0f;
        else
            return Convert.ToDouble(value, cultureInfo);
    }
}
