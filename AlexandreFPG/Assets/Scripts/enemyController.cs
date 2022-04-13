using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    float walking_speed = 2f;

    Transform target;
    //public Transform player;
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

        if (target)
        {

            transform.LookAt(new Vector3(target.position.x, transform.position.y,target.position.z))  ;

            Vector3 fromZombieToTarget = target.position - transform.position;
            fromZombieToTarget = new Vector3(fromZombieToTarget.x, 0f, fromZombieToTarget.z);
            Vector3 direction = fromZombieToTarget.normalized;

            transform.position += walking_speed * direction * Time.deltaTime;

        }
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
