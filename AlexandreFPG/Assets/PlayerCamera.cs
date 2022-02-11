using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour


{   enum camera_mode { Normal, Aiming, Transition_to_Aim, Transition_to_Normal }
    camera_mode is_currently = camera_mode.Normal;
    float angle = 0f, distance = 15f, camera_height = 5f;
    private Vector3 desired_camera_position;
    Transform owning_character_transform;
    private CharController owning_character;
    private float SENSITIVITY_VERTICAL_ROTATE = 0.003f;
    private float focal_height = 0.2f;

    Vector3 camera_aim_position = new Vector3(5, 1, -10);
    Quaternion camera_aim_orientation = Quaternion.Euler(0, -25, 0);
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //owning_character = FindObjectOfType<CharController>();
        //desired_camera_position = new Vector3(0, 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        switch (is_currently)
        {
            case camera_mode.Normal:
                transform.localPosition = Vector3.Lerp(transform.localPosition, desired_camera_position, 0.1f);
                transform.LookAt(owning_character_transform.position + focal_height * Vector3.up);

                break;

            case camera_mode.Transition_to_Aim:
                transform.localPosition = Vector3.Lerp(transform.localPosition, camera_aim_position, timer);
                transform.localRotation = Quaternion.Slerp(transform.rotation, camera_aim_orientation, timer);

                timer += Time.deltaTime;
                if (timer > 1.0f)
                    is_currently = camera_mode.Aiming;

                    

                break;

            case camera_mode.Transition_to_Normal:
                transform.localPosition = Vector3.Lerp(transform.localPosition, desired_camera_position, timer);
                transform.rotation = Quaternion.Slerp(transform.rotation, owning_character_transform.rotation, timer);

                timer += Time.deltaTime;
                if (timer > 1.0f)
                    is_currently = camera_mode.Normal;

                break;

            default:





                break;



        }


    }

    internal void start_aiming(bool aim_is_engaged)
    {
        if (aim_is_engaged)
        {
            if ((is_currently == camera_mode.Normal) || (is_currently == camera_mode.Transition_to_Normal))
                is_currently = camera_mode.Transition_to_Aim;
        }
        else
        {
            if ((is_currently == camera_mode.Transition_to_Aim) || (is_currently == camera_mode.Aiming))
                is_currently = camera_mode.Transition_to_Normal;
        }
    }

    internal void you_belong_to(CharController charController)
    {
        owning_character_transform = charController.transform;
        owning_character = charController;
    }

    internal void adjust_vertical_angle(float vertical_adjustment)
    {
        angle -= SENSITIVITY_VERTICAL_ROTATE * vertical_adjustment;
        angle = Mathf.Clamp(angle, -2, 0);

        desired_camera_position = camera_height * Vector3.up + new Vector3(0, distance * Mathf.Cos(angle), distance * Mathf.Sin(angle));
    }

}