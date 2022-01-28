using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            //GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject newZombie = Instantiate(zombie, transform.position, Quaternion.identity);

            newZombie.transform.position = new Vector3(Random.Range(0f, 50f), Random.Range(0f, 0f), Random.Range(0f, 50f));

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
