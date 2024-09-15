using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int Position; // ������� ����
    public bool Walkable; // ������������ ����
    public Node Parent; // ������������ ����

    // �����������
    public Node(Vector2Int position, bool walkable)
    {
        Position = position;
        Walkable = walkable;
        Parent = null; // ���������� ������������ ���� ����� null
    }
}
