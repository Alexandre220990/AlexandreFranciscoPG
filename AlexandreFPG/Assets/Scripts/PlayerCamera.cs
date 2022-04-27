using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour


{   
    float angle = 0f, distance = 10f, camera_height = 5f;
    private Vector3 desired_camera_position;
    public Transform owning_character_transform;
    private CharController owning_character;
    private float SENSITIVITY_VERTICAL_ROTATE = 0.003f;
  
    Transform character, focus;

    // Start is called before the first frame update
    void Start()
    {
        //owning_character = FindObjectOfType<CharController>();
        //desired_camera_position = new Vector3(0, 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desired_position = character.position - distance * character.forward + camera_height * Vector3.up;

        transform.position = Vector3.Lerp(transform.position, desired_position, 0.2f);
        transform.LookAt(focus.position, Vector3.up);

    }


    internal void adjust_vertical_angle(float vertical_adjustment)
    {
        angle -= SENSITIVITY_VERTICAL_ROTATE * vertical_adjustment;
        angle = Mathf.Clamp(angle, -2, 0);

        desired_camera_position = character.position + (camera_height * Vector3.up) - (distance * owning_character_transform.forward);
        
    }
    internal void Link(Transform character_transform, Transform crosshairs)
    {
        character = character_transform;
        focus = crosshairs;

    }

}