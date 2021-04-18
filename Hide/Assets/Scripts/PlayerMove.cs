using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMove : NetworkBehaviour
{
    public CharacterController controller;
    public GameObject bullet;
    public Transform FirePos;
    public GameObject body;

    public float jump = 7.0f;
    public float gravity = 9.8f;
    public float speed = 6.0f;
    public float bulletSpeed = 10f;

    private Vector3 direction = Vector3.zero;


    public override void OnStartClient()
    {
        transform.position = new Vector3(35, -5, 15);
    }


    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority) { return; }
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = new Vector3(35, -5, 15);
        }
        Move();
        Shoot();
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            direction = body.transform.TransformDirection(direction);
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

    // shoot
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawnBullet();
        }
    }

    [Command]
    private void spawnBullet()
    {
        GameObject newBullet;
        newBullet = Instantiate(bullet, FirePos.position, FirePos.rotation);
        NetworkServer.Spawn(newBullet);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * bulletSpeed;
        // Debug.Log("Shoot");
        Destroy(newBullet, 3f);
    }
}
