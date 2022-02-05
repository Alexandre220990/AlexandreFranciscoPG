using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private float current_speed;
    private float WALKING_SPEED = 3,RUNNING_SPEED = 6;
    private float turning_speed = 50;
    private float mouse_sensitivity_x = 120;

    public Rigidbody rb;



    Animator char_animation;

    PlayerCamera my_camera;

    // Start is called before the first frame update
    void Start()
    {
        current_speed = WALKING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
        my_camera = GetComponentInChildren<PlayerCamera>();
        my_camera.you_belong_to(this);

        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private bool should_jump()
    {
        return Input.GetButtonDown("Jump");
    }

    private void jump()
    {
        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);

        
    }
   

    private void adjust_camera(float vertical_adjustment)
    {
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
    private void Movement()
    {
        char_animation.SetBool("walking_forward", false);
        char_animation.SetBool("walking_backward", false);

        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();


        if (should_turn_left()) turn_left();
        if (should_turn_right()) turn_right();

        if (should_jump()) jump();

        turn(Input.GetAxis("Horizontal"));
        adjust_camera(Input.GetAxis("Vertical"));

    }
}
