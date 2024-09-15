using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main; // �������� �������� ������
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            // ������������� Z-���������� �� ������ ��������� ������� � ����
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

            // ����������� �������� ���������� � �������
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // ���������� ������ � ������ ��������
            transform.position = new Vector3(worldPosition.x - offset.x, worldPosition.y - offset.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true; // �������� ��������������
        Vector3 mousePosition = Input.mousePosition;
        // ������������� Z-���������� �� ������ ��������� ������� � ����
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

        // �������� ������� ������� ����
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // ��������� �������� ��� ����������� ��������������
        offset = new Vector3(worldPosition.x - transform.position.x, worldPosition.y - transform.position.y, 0);
    }

    private void OnMouseUp()
    {
        isDragging = false; // ���������� ��������������
    }

    private void OnMouseEnter()
    {
        // �������� ���� ��� ������ �������� ������� ��� ���������
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        // ���������� ���� ������� ��� ����� �������
        GetComponent<Renderer>().material.color = Color.white;
    }
}
