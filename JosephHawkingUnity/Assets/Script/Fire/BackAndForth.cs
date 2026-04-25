using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float sped = 3.0f;
    public float maxZ = 16.0f;
    public float minZ = -16.0f;

    // Двмжение в данный момент.
    private int _direction = 1;

    private void Update()
    {
        transform.Translate(0, 0, _direction * sped *  Time.deltaTime);

        bool bounced = false;
        if(transform.position.z > maxZ || transform.position.z < minZ)
        {
            // Меняем направление.
            _direction =- _direction;
            bounced = true;
        }
        // Делаем дополнительное движение в этом кадре, если объект поменял направление.
        if(bounced)
        {
            transform.Translate(0, 0, _direction * sped * Time.deltaTime);
        }
    }
}
