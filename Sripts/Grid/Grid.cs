using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int Width;                   // ������ �����
    public int Height;                  // ������ �����
    public float CellSize;              // ������ ������
    public GameObject cellPrefab;       // ������, ������� ����� �������� � ������ ������

    public Node[,] nodes;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        nodes = new Node[Width, Height]; // ������������� ������� �����
        Vector3 startPosition = transform.position; // �������� ���������� ������� ��� ���������

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                bool walkable = true;  // ����� ����� �������� ������, ����� ����������, ��������� �� ������
                Vector2Int position = new Vector2Int(x, y);
                nodes[x, y] = new Node(position, walkable); // �� ������ ������ Node ��� ���������� ����������.

                // ������������ ������� ��� ��������������� �������
                Vector3 prefabPosition = startPosition + new Vector3(x * CellSize, y * CellSize, 0);

                // ������������ ������ � ������������ � ����� ������ � ������������� ��������� ������� ������
                GameObject cellInstance = Instantiate(cellPrefab, prefabPosition, Quaternion.identity, transform);
                cellInstance.name = $"Cell {x} {y}";

                // ��� ����� � ���������� ���������� Node � ������
                // �����, ���� ��� ����� �������������� ������, ������� ������ ��������� �� �������.
                // Node node = cellInstance.GetComponent<Node>();
                // node.Position = position; // ������������� ������� ������ � �����
                // node.Walkable = walkable; // ������������� ������������
            }
        }
    }

    public Node GetNode(int x, int y)
    {
        // �������� ������
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            Debug.LogError($"Coordinates out of bounds: x: {x}, y: {y}");
            return null;
        }

        return nodes[x, y]; // ���������� ����
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startPosition = transform.position; // �������� ���������� ������� ��� ���������

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