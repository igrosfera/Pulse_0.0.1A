using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int Position; // Позиция узла
    public bool Walkable; // Проходимость узла
    public Node Parent; // Родительский узел

    // Конструктор
    public Node(Vector2Int position, bool walkable)
    {
        Position = position;
        Walkable = walkable;
        Parent = null; // Изначально родительский узел равен null
    }
}
