using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject[] objectsToToggle; // Массив объектов, которые мы хотим скрывать/отображать
    public bool isVisible = false; // Флаг видимости объектов

    void OnMouseDown()
    {
        // Переключение видимости при нажатии мыши
        isVisible = !isVisible;
        ToggleObjects(isVisible);
    }

    void ToggleObjects(bool visibility)
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null) // Проверяем, что объект существует
            {
                obj.SetActive(visibility); // Устанавливаем видимость объекта
            }
        }
    }
}
