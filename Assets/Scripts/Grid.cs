using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid1 {

    private int width;
    private int height;
    private int[,] gridArray;

    public Grid1(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int k = 0; k < gridArray.GetLength(1); k++)
            {
                Debug.Log(i + ", " + k);
            }
        }
    }

}
