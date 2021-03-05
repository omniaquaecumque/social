using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMove : NetworkBehaviour
{
    public CharacterController controller;
    public GameObject bullet;
    public Transform BulletSpawn;

    public float jump = 7.0f;
    public float gravity = 9.8f;
    public float speed = 6.0f;
    public float bulletSpeed = 10f;

    private Vector3 direction = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        if (isLocalPlayer)
        {
            if (controller.isGrounded)
            {
                direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                direction = transform.TransformDirection(direction);
                direction *= speed;
                if (Input.GetButton("Jump"))
                {
                    direction.y = jump;
                }
            }
            else
                direction.y -= gravity * Time.deltaTime;
            controller.Move(direction * Time.deltaTime);
        }
    }

    // shoot
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet;
            newBullet = Instantiate(bullet, BulletSpawn.position, BulletSpawn.rotation);
            newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletSpeed;
            Debug.Log("Shoot");
            Destroy(newBullet, 3f);
        }
    }

}
