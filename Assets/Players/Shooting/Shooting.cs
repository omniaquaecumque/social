using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectile; 
    public GameObject projectileSpawn; 
    public float fireRate;
    public float nextFire; 

    private Transform _projectile; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            Fire();
        }
    }

    void Fire() 
    {
        nextFire = Time.time + fireRate;
        _projectile = Instantiate(projectile.transform, projectileSpawn.transform.position, Quaternion.identity);
    }

}
