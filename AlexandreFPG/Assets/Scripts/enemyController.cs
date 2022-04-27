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
    Transform enemy;
    //NavMeshAgent agent;

    private ManagerScript the_manager;

    enum enemy_State { move_to_player, Attacking, Dying }
    [SerializeField]
    enemy_State _currentState = enemy_State.move_to_player;
    Animator enemy_animation;



    // Start is called before the first frame update
    void Start()
    {
        //target = ManagerScript.instance.mainChar.tranform;
        //agent = GetComponent<NavMeshAgent>();
        target = the_manager.mainChar.transform;
        enemy = the_manager.zombie.transform;

        enemy_animation = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        /*https://www.mpgh.net/forum/512-unity-udk-gamestudio-cryengine-development/427244-unity-enemy-ai-health-attack-scripts-c.html*/
        //*
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float direct = Vector3.Dot(dir, transform.forward);


        if (distance < 2.5f)
        {
            if (direct > 0)
            {
                _currentState = enemy_State.Attacking;
            }
            
        }
        else
            _currentState = enemy_State.move_to_player;
        //*

        switch (_currentState)
        {
            case enemy_State.move_to_player:

                enemy_animation.SetBool("walking_forward", true);
                //enemy_animation.SetBool("is_attacking", false);

                target = the_manager.find_me_a_target();

                if (target)
                {

                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

                    Vector3 fromZombieToTarget = target.position - transform.position;
                    fromZombieToTarget = new Vector3(fromZombieToTarget.x, 0f, fromZombieToTarget.z);
                    Vector3 direction = fromZombieToTarget.normalized;

                    transform.position += walking_speed * direction * Time.deltaTime;

                }

                break;
            case
                enemy_State.Attacking:



                enemy_animation.SetBool("is_attacking", true);
                //enemy_animation.SetBool("walking_forward", false);



                break;
            case
                enemy_State.Dying:

                enemy_animation.SetBool("is_Dead", true);

                break;

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
