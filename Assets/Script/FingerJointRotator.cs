using UnityEngine;

public class FingerJointRotator : MonoBehaviour
{
    public Transform targetBone; // The bone corresponding to this sphere
    public float rotationSpeed = 5f; // Speed of bone rotation based on sphere movement
    
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 previousMousePosition;

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera reference
    }

    void Update()
    {
        if (isDragging)
        {
            // Get the current mouse position in world space
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // Keep the distance consistent
            Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

            // Calculate how much the sphere has moved
            Vector3 delta = worldMousePosition - previousMousePosition;

            // Rotate the bone based on the mouse movement
            RotateBone(delta);

            // Update the previous mouse position for the next frame
            previousMousePosition = worldMousePosition;
        }

        // Start dragging when mouse button is pressed
        if (Input.GetMouseButtonDown(0) && IsMouseOverSphere())
        {
            isDragging = true;
            previousMousePosition = mainCamera.WorldToScreenPoint(transform.position);
        }

        // Stop dragging when mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private bool IsMouseOverSphere()
    {
        // Check if the mouse is over the sphere using a Raycast or Collider
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                return true;
            }
        }

        return false;
    }

    private void RotateBone(Vector3 delta)
    {
        // Here you can define how the bone rotates. For example, use the delta to rotate along the Y axis.
        Vector3 axis = transform.position - targetBone.position; // Get the direction vector to the bone
        axis = axis.normalized; // Normalize it so it’s a unit vector
        
        // Apply the rotation to the bone based on the delta movement
        float angle = delta.magnitude * rotationSpeed; // The angle change depends on the movement distance
        targetBone.Rotate(axis, angle, Space.World); // Rotate the bone in world space
    }
}
