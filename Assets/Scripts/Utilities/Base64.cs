using System;
using System.Text;

public static class Base64
{
    public static string Encode(string text)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);

        return Convert.ToBase64String(textBytes);
    }

    public static string Decode(string base64EncodedData)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
