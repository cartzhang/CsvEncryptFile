using System;
using System.Text;
using UnityEngine;
using System.Collections;

public class Base64{
    public static string EncodeBase64(Encoding encode, string source)
    {
        string result;
        byte[] bytes = encode.GetBytes(source);
        try
        {
            result = Convert.ToBase64String(bytes);
        }
        catch
        {
            result = source;
        }
        return result;
    }

    public static string EncodeBase64(string source)
    {
        return EncodeBase64(Encoding.UTF8, source);
    }

    public static string DecodeBase64(Encoding encode, string result)
    {
        string decode = "";
        byte[] bytes = Convert.FromBase64String(result);
        try
        {
            decode = encode.GetString(bytes);
        }
        catch
        {
            decode = result;
        }
        return decode;
    }

    public static string DecodeBase64(string result)
    {
        return DecodeBase64(Encoding.UTF8, result);
    }
}
