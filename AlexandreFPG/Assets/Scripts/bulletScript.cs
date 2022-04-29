using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    private float bullet_speed = 100f;

    private void Update()
    {
        transform.position += bullet_speed * transform.forward * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {

        print("ddddd");
        if (other.gameObject.tag == "Enemy")

        {
            Destroy(gameObject);
        }

    }
}
