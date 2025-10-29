using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class Formater
{
    public static string[] Postfixes = new[]
    {
        "","K", "M", "B", "T", "Q", "s", "S", "o", "n", "d", "U", "D", "t",
    };
    public static string Format(double value)
    {
        int postfixIndex = 0;
        for (int i = 0; i < Postfixes.Length; i++)
        {
            if (value >= 1000)
            {
                value /= 1000;
                postfixIndex++;
            }
            else
            {
                break;
            }
        }
        string postfix = Postfixes[postfixIndex];
        return value.ToString("0.##", CultureInfo.CreateSpecificCulture("en-GB")) + postfix; //"0.##" - числа после точки будут написаны, если они не нули. Если нули, то написаны не будут. CultureInfo.CreateSpecificCulture("en-GB") - без него дробные части будут отделяться запятой, а не точкой
    }
}
