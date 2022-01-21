using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private float current_speed;
    private float WALKING_SPEED = 1, RUNNING_SPEED = 3;
    private float turning_speed = 90;
    private float mouse_sensitivity_x = 180;
    Animator char_animation;
    // Start is called before the first frame update
    void Start()
    {
        current_speed = WALKING_SPEED;
        char_animation = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        char_animation.SetBool("walking_forward", false);
        char_animation.SetBool("walking_backward", false);

        if (should_move_forward()) move_foward();
        if (should_move_backward()) move_backward();

        if (should_turn_left()) turn_left();

        turn(Input.GetAxis("Horizontal"));


    }

    private void turn(float mouse_turn_value_x)
    {
        transform.Rotate(Vector3.up, mouse_sensitivity_x * mouse_turn_value_x * Time.deltaTime);
        if (Mathf.Abs(mouse_turn_value_x) > 0.5f) char_animation.SetBool("walking_backward", true);
    }

    private void turn_left()
    {
        transform.Rotate(Vector3.up, -turning_speed * Time.deltaTime);
        char_animation.SetBool("walking_backward", true);
    }

    private bool should_turn_left()
    {
        return Input.GetKey(KeyCode.A);

    }

    private void move_backward()
    {
        // move in F.R.I. using s = u*t 
        transform.position -= current_speed * transform.forward * Time.deltaTime;

        char_animation.SetBool("walking_backward", true);
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }

    private void move_foward()
    {   // move in F.R.I. using s = u*t 
        transform.position += current_speed * transform.forward * Time.deltaTime;
        char_animation.SetBool("walking_forward", true);

    }

    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }
}