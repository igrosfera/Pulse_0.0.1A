using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject[] objectsToToggle; // ������ ��������, ������� �� ����� ��������/����������
    public bool isVisible = false; // ���� ��������� ��������

    void OnMouseDown()
    {
        // ������������ ��������� ��� ������� ����
        isVisible = !isVisible;
        ToggleObjects(isVisible);
    }

    void ToggleObjects(bool visibility)
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null) // ���������, ��� ������ ����������
            {
                obj.SetActive(visibility); // ������������� ��������� �������
            }
        }
    }
}
