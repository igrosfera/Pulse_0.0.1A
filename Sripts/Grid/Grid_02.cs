using System.Collections.Generic;
using UnityEngine;

public class Grid_02 : MonoBehaviour
{
    public GameObject cellPrefab;                       // ������, ������� ����� �������� � ������ ������
    public int[,] gridMatrix;                           // ������� ��� ������� �����

    private Node[,] nodes;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        if (gridMatrix == null) return;                 // �������� �� ������, ���� ������� �� ����������������

        int width = gridMatrix.GetLength(0);            // ������ �������
        int height = gridMatrix.GetLength(1);           // ������ �������
        nodes = new Node[width, height];

        Vector3 startPosition = transform.position;      // �������� ���������� ������� ��� ���������

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridMatrix[x, y] == 1)                // �������� �������� � �������
                {
                    bool walkable = true;                 // ����� ����� �������� ������, ����� ����������, ��������� �� ������
                    Vector2Int position = new Vector2Int(x, y);
                    nodes[x, y] = new Node(position, walkable);

                    // ������������ ������� ��� ��������������� �������
                    Vector3 prefabPosition = startPosition + new Vector3(x, y, 0);
                    Instantiate(cellPrefab, prefabPosition, Quaternion.identity);

                    // ����� ��������� ������ � ������ ��������� �������
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
        Vector3 startPosition = transform.position; // �������� ���������� ������� ��� ���������

        if (gridMatrix == null) return;              // �������� �� ������, ���� ������� �� ����������������

        int width = gridMatrix.GetLength(0);
        int height = gridMatrix.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridMatrix[x, y] == 1)             // ������ ������ ���������� ������
                {
                    Vector3 position = startPosition + new Vector3(x, y, 0);
                    Gizmos.DrawWireCube(position, new Vector3(1, 1, 0.1f));
                }
            }
        }
    }
}