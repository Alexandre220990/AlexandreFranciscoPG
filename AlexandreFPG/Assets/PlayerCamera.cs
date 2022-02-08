using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    float angle = 0f, distance = 15f, camera_height = 5f;
    private Vector3 desired_camera_position;
    Transform owning_character_transform;
    private CharController owning_character;
    private float SENSITIVITY_VERTICAL_ROTATE = 0.003f;
    private float focal_height = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //owning_character = FindObjectOfType<CharController>();
        //desired_camera_position = new Vector3(0, 1, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desired_camera_position, 0.1f);
        transform.LookAt(owning_character_transform.position + focal_height * Vector3.up);
        print(Mathf.Rad2Deg * angle);
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