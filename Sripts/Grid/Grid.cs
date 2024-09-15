using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int Width;                   // Ширина сетки
    public int Height;                  // Высота сетки
    public float CellSize;              // Размер ячейки
    public GameObject cellPrefab;       // Префаб, который будет размещён в каждой ячейке

    public Node[,] nodes;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        nodes = new Node[Width, Height]; // Инициализация массива узлов
        Vector3 startPosition = transform.position; // Получаем координаты объекта как начальные

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                bool walkable = true;  // Здесь можно добавить логику, чтобы определить, проходима ли ячейка
                Vector2Int position = new Vector2Int(x, y);
                nodes[x, y] = new Node(position, walkable); // Мы создаём объект Node без добавления компонента.

                // Рассчитываем позицию для инстанцирования префаба
                Vector3 prefabPosition = startPosition + new Vector3(x * CellSize, y * CellSize, 0);

                // Инстанцируем префаб в соответствии с углом ячейки и устанавливаем родителем текущий объект
                GameObject cellInstance = Instantiate(cellPrefab, prefabPosition, Quaternion.identity, transform);
                cellInstance.name = $"Cell {x} {y}";

                // Нет нужды в добавлении компонента Node в префаб
                // Можно, если вам нужны дополнительные данные, создать другой компонент на префабе.
                // Node node = cellInstance.GetComponent<Node>();
                // node.Position = position; // Устанавливаем позицию ячейки в сетке
                // node.Walkable = walkable; // Устанавливаем проходимость
            }
        }
    }

    public Node GetNode(int x, int y)
    {
        // Проверка границ
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            Debug.LogError($"Coordinates out of bounds: x: {x}, y: {y}");
            return null;
        }

        return nodes[x, y]; // Возвращаем узел
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startPosition = transform.position; // Получаем координаты объекта как начальные

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3 position = startPosition + new Vector3(x * CellSize, y * CellSize, 0);
                Gizmos.DrawWireCube(position, new Vector3(CellSize, CellSize, 0.1f));
            }
        }
    }
}