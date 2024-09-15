using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Grid grid; // Ссылка на сетку
    public float speed = 5f; // Скорость юнита
    private List<Node> path; // Путь для перемещения

    void Start()
    {
        grid = GetComponent<Grid>();
    }

    public void MoveToTarget(Vector2Int targetPosition)
    {
        Node startNode = grid.GetNode(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        Node targetNode = grid.GetNode(targetPosition.x, targetPosition.y);

        if (startNode == null || targetNode == null || !targetNode.Walkable)
        {
            Debug.LogError("Invalid start or target node.");
            return;
        }

        path = FindPath(startNode.Position, targetNode.Position);
        if (path == null)
        {
            Debug.LogError("No path found!");
            return;
        }

        StartCoroutine(MoveAlongPath());
    }

    public List<Node> FindPath(Vector2Int start, Vector2Int target)
    {
        Node startNode = grid.GetNode(start.x, start.y);
        Node targetNode = grid.GetNode(target.x, target.y);

        if (startNode == null || targetNode == null)
        {
            Debug.LogError($"Start node or target node is null.");
            return null;
        }

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (GetDistance(openSet[i], targetNode) < GetDistance(currentNode, targetNode))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.Walkable || closedSet.Contains(neighbor))
                    continue;

                if (!openSet.Contains(neighbor))
                {
                    openSet.Add(neighbor);
                    neighbor.Parent = currentNode;
                }
            }
        }

        return null;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        if (node == null) return neighbors;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Node neighbor = grid.GetNode(node.Position.x + x, node.Position.y + y);
                if (neighbor != null)
                {
                    neighbors.Add(neighbor);
                }
            }
        }

        return neighbors;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Reverse();
        return path;
    }

    int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.Position.x - b.Position.x);
        int dstY = Mathf.Abs(a.Position.y - b.Position.y);
        return 14 * Mathf.Min(dstX, dstY) + 10 * Mathf.Abs(dstX - dstY);
    }

    IEnumerator MoveAlongPath()
    {
        foreach (Node node in path)
        {
            Vector3 start = transform.position;
            Vector3 end = new Vector3(node.Position.x, node.Position.y, 0);
            float journeyLength = Vector3.Distance(start, end);
            float startTime = Time.time;

            while (Vector3.Distance(transform.position, end) > 0.1f)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(start, end, fractionOfJourney);
                yield return null;
            }

            transform.position = end; // Устанавливаем в конечную позицию
        }
    }
}