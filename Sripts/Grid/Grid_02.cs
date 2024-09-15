using System.Collections.Generic;
using UnityEngine;

public class Grid_02 : MonoBehaviour
{
    public GameObject cellPrefab;                       // Префаб, который будет размещён в каждой ячейке
    public int[,] gridMatrix;                           // Матрица для задания сетки

    private Node[,] nodes;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        if (gridMatrix == null) return;                 // Проверка на случай, если матрица не инициализирована

        int width = gridMatrix.GetLength(0);            // Ширина матрицы
        int height = gridMatrix.GetLength(1);           // Высота матрицы
        nodes = new Node[width, height];

        Vector3 startPosition = transform.position;      // Получаем координаты объекта как начальные

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridMatrix[x, y] == 1)                // Проверка значения в матрице
                {
                    bool walkable = true;                 // Здесь можно добавить логику, чтобы определить, проходима ли ячейка
                    Vector2Int position = new Vector2Int(x, y);
                    nodes[x, y] = new Node(position, walkable);

                    // Рассчитываем позицию для инстанцирования префаба
                    Vector3 prefabPosition = startPosition + new Vector3(x, y, 0);
                    Instantiate(cellPrefab, prefabPosition, Quaternion.identity);

                    // Вывод координат ячейки с учетом начальной позиции
                    Debug.Log($"Node at: {prefabPosition}");
                }
            }
        }
    }

    public Node GetNode(int x, int y)
    {
        if (x >= 0 && x < nodes.GetLength(0) && y >= 0 && y < nodes.GetLength(1))
        {
            return nodes[x, y];
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startPosition = transform.position; // Получаем координаты объекта как начальные

        if (gridMatrix == null) return;              // Проверка на случай, если матрица не инициализирована

        int width = gridMatrix.GetLength(0);
        int height = gridMatrix.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridMatrix[x, y] == 1)             // Рисуем только проходимые ячейки
                {
                    Vector3 position = startPosition + new Vector3(x, y, 0);
                    Gizmos.DrawWireCube(position, new Vector3(1, 1, 0.1f));
                }
            }
        }
    }
}