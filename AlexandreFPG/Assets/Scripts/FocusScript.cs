using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusScript : MonoBehaviour
{
    private float sensitivity = 0.01f;
    Vector3 local_start;
    Transform owner;

    private float distance_to_crosshair = 50;
    float elevation;

    // SVtart is called before the first frame update
    void Start()
    {
        
        local_start = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
     
    }

    internal void adjust_vertical_angle(float vertical_adjustment)
    {
        float newY = Mathf.Clamp(transform.localPosition.y + sensitivity * vertical_adjustment, 0, 10);
        transform.localPosition = new Vector3(local_start.x, newY, local_start.z);

    }
    internal void starting_setup(Transform character)
    {

        owner = character;
    }
}
