using UnityEngine;

public class RotationVisualizer : MonoBehaviour
{
    public float radius = 1f;           // Radius of the rings
    public float ringResolution = 30;   // Number of segments for the ring (higher = smoother)
    public float ringThickness = 0.05f; // Thickness of the rings

    private void OnDrawGizmos()
    {
        // Draw the rings centered around the object, aligned to X, Y, and Z axes
        Gizmos.matrix = transform.localToWorldMatrix;  // Apply object's world matrix to position rings correctly

        DrawRing(Vector3.right, Color.red);  // X-axis (Red)
        DrawRing(Vector3.up, Color.green);   // Y-axis (Green)
        DrawRing(Vector3.forward, Color.blue); // Z-axis (Blue)
    }

    void DrawRing(Vector3 axis, Color color)
    {
        Gizmos.color = color;
        Vector3 previousPoint = Vector3.zero;

        // Create a circle in the plane perpendicular to the axis
        for (int i = 0; i <= ringResolution; i++)
        {
            float angle = i * Mathf.PI * 2f / ringResolution;

            // Calculate point on the circle
            Vector3 point = Vector3.zero;

            // If the axis is X (right), make the circle in the YZ plane
            if (axis == Vector3.right)
            {
                point = new Vector3(0, Mathf.Cos(angle), Mathf.Sin(angle));
            }
            // If the axis is Y (up), make the circle in the XZ plane
            else if (axis == Vector3.up)
            {
                point = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            }
            // If the axis is Z (forward), make the circle in the XY plane
            else if (axis == Vector3.forward)
            {
                point = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            }

            point *= radius;

            // Draw the circle in the scene using lines
            if (i > 0)
            {
                Gizmos.DrawLine(previousPoint, point);
            }

            previousPoint = point;
        }
    }
}
