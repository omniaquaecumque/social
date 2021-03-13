using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float aliveTime; 
    public float moveSpeed;

    public GameObject projectileSpawn;

    // Start is called before the first frame update
    void Start()
    {
        projectileSpawn = GameObject.Find("ProjectileSpawn");
        this.transform.rotation = projectileSpawn.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        aliveTime -= 1 * Time.deltaTime; 
        if (aliveTime <= 0)
        {
            Destroy(this.gameObject);
        }

        this.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);   
    }

    void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "Shield")
            {
                Destroy(this.gameObject);
            }
    }      
}
