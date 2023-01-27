using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGeneratorCopyOld
{
    public static float[,] GenerateFalloffMap(int size)
    {
        float[,] map = new float[size, size];

        //two for-loops for the x and z (length x width)
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //times 2 minus 1 because we also want a range from -1 to 1
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                //find out which one is closest to the edge of the square
                //to use for our map
                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(value);
            }
        }
        return map;
    }

    static float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;
        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}