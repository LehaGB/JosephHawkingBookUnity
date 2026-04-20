using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject fireBallPrefab;
    private GameObject _fireBall;

    // Слежение за состоянием персонажа.
    private bool _alive;

    /*Значения для скорости движения 
    и расстояния, с которого начинается 
    реакция на препятствие.
     */ 
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    private void Start()
    {
        _alive = true;
    }

    void Update()
    {
        // Движение начинается только в случае живого персонажа.
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            // Луч находится и нацеливается в том же положении, что и персонаж.
            Ray ray = new Ray(transform.position, transform.forward);

            // Хранения данных луча.
            RaycastHit hit;

            // Бросаем луч с описанной 
            // вокруг него окружностью.
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if(hitObject.GetComponent<PlayerCharacter>())
                {
                    if(_fireBall == null)
                    {
                        _fireBall = Instantiate(fireBallPrefab) as GameObject;

                        // Поместим огненный шар перед врагом и нацелим в направлении 
                        // его движения.
                        _fireBall.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireBall.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    // Поворот с наполовину случайным  выбором нового направления.
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }


    // Открытый метод, позволяющий внешнему коду воздействовать
    // на «живое» состояние.
    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
