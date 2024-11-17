using System;

namespace LenovoLegionToolkit.Lib.Extensions;

public static class MathExtensions
{
    public static int RoundNearest(int value, int factor)
    {
        return (int)Math.Round(value / (double)factor, MidpointRounding.AwayFromZero) * factor;
    }
    public static byte DivideBy(this byte a, int b) 
    { 
        if (b == 0) 
        { 
            throw new DivideByZeroException("Denominator cannot be zero"); 
        }
        int result = a / b; 
        if (result > byte.MaxValue || result < byte.MinValue) 
        { 
            throw new OverflowException("Resulting value is out of the byte range"); 
        }
        return (byte)result; 
    }
}
