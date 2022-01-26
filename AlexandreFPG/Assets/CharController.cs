using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private float current_speed;
    private float WALKING_SPEED = 1,RUNNING_SPEED = 3;
    private float turning_speed = 90;
    private float mouse_sensitivity_x = 180;
    // Start is called before the first frame update
    void Start()
    {
        current_speed = WALKING_SPEED;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (should_move_forward()) move_forward();
        if (should_move_backward()) move_backward();
        if (should_turn_left()) turn_left();
        if (should_turn_right()) turn_right();

        turn(Input.GetAxis("Horizontal"));
    }

    private void turn(float mouse_turn_value_x)
    {
        transform.Rotate(Vector3.up, mouse_sensitivity_x * mouse_turn_value_x * Time.deltaTime);
    }

    private void turn_right()
    {
        transform.Rotate(Vector3.up, -turning_speed * Time.deltaTime);

    }
    private bool should_turn_right()
    {
        return Input.GetKey(KeyCode.D);
    }

    private void turn_left()
    {
        transform.Rotate(Vector3.down, -turning_speed * Time.deltaTime);
       
    }

    private bool should_turn_left()
    {
        return Input.GetKey(KeyCode.A);

    }
    private void move_backward()
    {
        transform.position -= current_speed * transform.forward * Time.deltaTime;
    }

    private bool should_move_backward()
    {
        return Input.GetKey(KeyCode.S);
    }
    private void move_forward()
    {
        //move in F.R.I using s = u * p
        transform.position += current_speed * transform.forward * Time.deltaTime;
    }

    private bool should_move_forward()
    {
        return Input.GetKey(KeyCode.W);
    }
}
