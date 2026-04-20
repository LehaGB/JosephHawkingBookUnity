using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();

        // Скрываем указатель мыши в центре экрана.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Выстрел.
        if (Input.GetMouseButtonDown(0))
        {
            // Середина экрана.
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);

            // Создание в этой точке луча методом ScreenPointToRay(point);
            Ray ray = _camera.ScreenPointToRay(point);

            // Переменная для хранение данных луча.
            RaycastHit hit;

            // Испущенный луч заполняет информацией переменную, на которую имеется ссылка.
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget reactiveTarget = hitObject.GetComponent<ReactiveTarget>();
                if(reactiveTarget != null)
                {
                    reactiveTarget.ReactToHit();
                }
                else
                {
                    // Загружаем координаты точки, в которую попал луч.
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }           
        }
    }


    // Отпечаток пули.
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }


    // Прицел.
    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;


        // Команда GUI.Label() отображает на экране символ.
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
