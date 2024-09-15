using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Grid grid;
    public Pathfinding pathfinding;
    public bool isSelected = false;

    void Update()
    {
        if (isSelected && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Vector2Int clickedNodePosition = new Vector2Int(
                    Mathf.FloorToInt((hit.point.x - grid.transform.position.x) / grid.CellSize),
                    Mathf.FloorToInt((hit.point.y - grid.transform.position.y) / grid.CellSize)
                );

                if (IsWithinGridBounds(clickedNodePosition))
                {
                    Node selectedNode = grid.GetNode(clickedNodePosition.x, clickedNodePosition.y);

                    if (selectedNode != null && selectedNode.Walkable)
                    {
                        pathfinding.MoveToTarget(selectedNode.Position);
                    }
                    else
                    {
                        Debug.LogError($"Selected node at {clickedNodePosition.x}, {clickedNodePosition.y} is null or not walkable.");
                    }
                }
                else
                {
                    Debug.LogError($"Clicked position {clickedNodePosition} is out of grid bounds.");
                }
            }
        }
    }

    private bool IsWithinGridBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.Width && position.y >= 0 && position.y < grid.Height;
    }

    void OnMouseDown()
    {
        isSelected = !isSelected;
    }
}