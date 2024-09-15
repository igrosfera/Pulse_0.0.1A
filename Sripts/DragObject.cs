using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main; // Получаем основную камеру
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            // Устанавливаем Z-координату на основе положения объекта в мире
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

            // Преобразуем экранные координаты в мировые
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Перемещаем объект с учетом смещения
            transform.position = new Vector3(worldPosition.x - offset.x, worldPosition.y - offset.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        isDragging = true; // Начинаем перетаскивание
        Vector3 mousePosition = Input.mousePosition;
        // Устанавливаем Z-координату на основе положения объекта в мире
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;

        // Получаем мировую позицию мыши
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Вычисляем смещение для корректного перетаскивания
        offset = new Vector3(worldPosition.x - transform.position.x, worldPosition.y - transform.position.y, 0);
    }

    private void OnMouseUp()
    {
        isDragging = false; // Прекращаем перетаскивание
    }

    private void OnMouseEnter()
    {
        // Изменяем цвет или другие свойства объекта при наведении
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        // Возвращаем цвет обратно при уходе курсора
        GetComponent<Renderer>().material.color = Color.white;
    }
}
