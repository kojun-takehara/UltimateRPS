using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{
    public float rotationSpeed = 50f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the sphere when Space key is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Rotate around the Y-axis for this example (you can change this as needed)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}