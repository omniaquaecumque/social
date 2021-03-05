using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class bulletBeh : NetworkBehaviour
{
    public float speed = 10f;
    public float time = 20f;
    void Update()
    {
        /*
        transform.position += transform.forward * speed * Time.deltaTime;
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            Destroy(gameObject);
        }
        */
    }
}
