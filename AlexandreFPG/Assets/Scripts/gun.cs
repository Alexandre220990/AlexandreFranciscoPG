using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{

    float bulletSpeed = 1000;
    public GameObject bullet;
    
    void ShootWeapon()
    {
        GameObject tempBullet = Instantiate(bullet, transform.position,transform.rotation) as GameObject;
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(transform.up * bulletSpeed);
        Destroy(tempBullet, 10f);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootWeapon();

        }
    }
}
