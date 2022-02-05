using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    //NavMeshAgent agent;

    private ManagerScript the_manager;

    Animator enemy_animation;

    // Start is called before the first frame update
    void Start()
    {
        //target = ManagerScript.instance.mainChar.tranform;
        //agent = GetComponent<NavMeshAgent>();

        enemy_animation = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy_animation.SetBool("walking_forward", true);

        if (Input.GetKeyDown(KeyCode.J))
            target = the_manager.find_me_a_target();

        transform.position = Vector3.Lerp(transform.position, target.position, 0.001f);

            transform.LookAt(target.transform);
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    */

    internal void im_the_manager(ManagerScript managerScript)
    {
        the_manager = managerScript;
    }
}
