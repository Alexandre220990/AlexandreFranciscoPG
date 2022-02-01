using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject zombie;
    public GameObject mainChar;
    List<enemyController> zombies;
    List<CharacterController> players;
    public static object instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        zombies = new List<enemyController>();

        for (int i = 0; i < 100; i++)
        {
            //GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject newzombieGO = Instantiate(zombie, transform.position, Quaternion.identity);

            newzombieGO.transform.position = new Vector3(Random.Range(0f, 50f), Random.Range(0f, 0f), Random.Range(0f, 50f));

            enemyController new_zombie = newzombieGO.GetComponent<enemyController>();

            zombies.Add(new_zombie);

            new_zombie.im_the_manager(this);


        }
    }

    internal Transform find_me_a_target()
    {
        return mainChar.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
