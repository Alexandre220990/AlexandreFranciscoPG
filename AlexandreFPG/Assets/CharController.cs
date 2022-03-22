using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    bool isGrounded=true;
    private float current_speed;
    private float WALKING_SPEED = 3, RUNNING_SPEED = 10;
    private float turning_speed = 50;
    private float mouse_sensitivity_x = 60;

    public float damage = 10;
    public float distance = 100f;

    
     Rigidbody rb;


    GameObject see_cube;
    Animator char_animation;

    PlayerCamera my_camera;
    FocusScript my_focus;

    // Start is called before the first frame update
    void Start()
    {
        //see_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //see_cube.GetComponent<Collider>().enabled = false;
        current_speed = WALKING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
        my_camera = FindObjectOfType<PlayerCamera>();

        my_focus = GetComponentInChildren<FocusScript>();
        my_camera.you_belong_to(this, my_focus);

        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
       
    }
    private void Movement()
    {
        char_animation.SetBool("walking_forward", false);
        char_animation.SetBool("walking_backward", false);

        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();


        if (should_turn_left()) turn_left();
        if (should_turn_right()) turn_right();

        if (should_jump()) jump();
     
        if (should_run()) run();
        if (should_walk()) walk();


        aim(should_aim());
        

         if (should_attack()) attack();

        turn(Input.GetAxis("Horizontal"));
        adjust_camera(Input.GetAxis("Vertical"));

        if (!isGrounded) // && rb.velocity.y  < 0.1f)
            isGrounded = check_if_grounded();
    }

    private void aim(bool continue_aiming)
    {
        my_camera.start_aiming(continue_aiming);

    }

    private bool should_aim()
    {
       return Input.GetMouseButton(2);
    }

    private void attack()
    {
        RaycastHit hit;
        if(Physics.Raycast(my_camera.transform.position, my_camera.transform.forward, out hit, distance))
        {

        }
    }

    private bool should_attack()
    {
        return Input.GetButtonDown("Fire1");
    }

    private bool check_if_grounded()
    {   
       Collider[] cols = Physics.OverlapBox(transform.position - new Vector3(0, 2f, 0), new Vector3(0.5f, 0.5f, 0.5f));
        //see_cube.transform.position = rb.position - new Vector3(0, 2f, 0);
        foreach (Collider c in cols)
        {
            if (c.tag == "ground") return true;
        }

        return false;
    }

    private bool should_jump()
    {
        return Input.GetButtonDown("Jump");
    }

    private void jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isGrounded = false;
        }
    }


    private void adjust_camera(float vertical_adjustment)
    {
        my_focus.adjust_vertical_angle(vertical_adjustment);

        my_camera.adjust_vertical_angle(vertical_adjustment);
    }

    private void turn(float mouse_turn_value_x)
    {
        transform.Rotate(Vector3.up, mouse_sensitivity_x * mouse_turn_value_x * Time.deltaTime);
        if (Mathf.Abs(mouse_turn_value_x) > 0.5f) char_animation.SetBool("walking_backward", true);
    }

    private void turn_right()
    {
        transform.Rotate(Vector3.up, -turning_speed * Time.deltaTime);

    }
    private bool should_turn_right()
    {
        return Input.GetKey(KeyCode.A);
    }

    private void turn_left()
    {
        transform.Rotate(Vector3.down, -turning_speed * Time.deltaTime);
        char_animation.SetBool("walking_backward", true);

    }

    private bool should_turn_left()
    {
        return Input.GetKey(KeyCode.D);

    }
    private void move_backward()
    {
        transform.position -= current_speed * transform.forward * Time.deltaTime;
        char_animation.SetBool("walking_backward", true);
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }
    private void move_forward()
    {
        //move in F.R.I using s = u * p
        transform.position += current_speed * transform.forward * Time.deltaTime;
        char_animation.SetBool("walking_forward", true);
    }

    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }

    private void walk()
    {
        current_speed = WALKING_SPEED;
    }

    private bool should_walk()
    {
        return Input.GetKeyDown(KeyCode.R);
    }

    private void run()
    {
        current_speed = RUNNING_SPEED;
    }

    private bool should_run()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}