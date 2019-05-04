using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixSolver
{
    public static float[,] matrix = new float[2, 2];

    private static float a;
    private static float b;
    private static float c;
    private static float d;

    public static float x = 45.2f;
    public static float y = 26.2f;

    public static Vector2Int SolveFor(Vector2 pos)
    {
        a = x;
        b = x;
        c = -y;
        d = y;

        matrix[0, 0] = c;
        matrix[1, 0] = d;
        matrix[0, 1] = a;
        matrix[1, 1] = b;

        return SolveMatrix(pos.x, pos.y);
    }

    public static Vector2Int SolveMatrix(float posX, float posY)
    {
        var det = 1 / ((a * d) - (b * c));
        matrix[0, 0] = -c * det;
        matrix[1, 0] = a * det;
        matrix[0, 1] = d * det;
        matrix[1, 1] = -b * det;

        var x = Mathf.RoundToInt((matrix[0, 1] * (100 * posX)) + (matrix[1, 1] * (posY * 100)));
        var y = Mathf.RoundToInt((matrix[0, 0] * (100 * posX)) + (matrix[1, 0] * (posY * 100)));
        return new Vector2Int(x, y);
    }
}
