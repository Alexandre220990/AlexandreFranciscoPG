using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour, IDamageable
{
    public float lookRadius = 10f;
    float walking_speed = 2f;

    Transform target;
    Transform enemy;
    //NavMeshAgent agent;

    private ManagerScript the_manager;
   

    public int MaxHP = 40;
    public int CurrentHP;

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
        CurrentHP = MaxHP;
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


        if (distance < 1.5f)
        {
            if (direct > 0)
            {
                _currentState = enemy_State.Attacking;
            }           
        }
        else
            _currentState = enemy_State.move_to_player;
        //*
        if (CurrentHP <= 0)
        {
            _currentState = enemy_State.Dying;
            Destroy(gameObject, 10f);
        }

        switch (_currentState)
        {
            case enemy_State.move_to_player:

                enemy_animation.SetBool("walking_forward", true);

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

                break;
            case
                enemy_State.Dying:

                enemy_animation.SetBool("is_Dead", true);

                break;
        }
    }

    internal void im_the_manager(ManagerScript managerScript)
    {
        the_manager = managerScript;
    }


    private void OnTriggerEnter(Collider shoot)
    {
        if (shoot.gameObject.tag == "bullet")
        {
            print("Hit zombie");
            takeDamage(20);
        }
    }

    public void takeDamage(int Dmg)
    {
        print("Damage");
        CurrentHP -= Dmg;  
    }
    
}
