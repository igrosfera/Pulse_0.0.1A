using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Matrixs : MonoBehaviour
{
    public int[,] gridMatrix = new int[,]
    {
         { 1, 1, 0, 0, 0 },
         { 1, 0, 1, 1, 0 },
         { 0, 1, 1, 0, 1 },
         { 0, 0, 0, 1, 1 },
         { 1, 1, 0, 0, 1 }
    };
}
