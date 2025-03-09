using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RuntimeRotationTool : MonoBehaviour
{
    public float ringRadius = 1.5f;
    public float ringThickness = 0.05f;
    public Color xAxisColor = Color.red;
    public Color yAxisColor = Color.green;
    public Color zAxisColor = Color.blue;
    public int segments = 100; // Number of segments for the ring mesh

    private LineRenderer xRing;
    private LineRenderer yRing;
    private LineRenderer zRing;

    private Transform selectedRing;
    private Vector3 rotationAxis;
    private Vector3 initialMouseDirection;
    private Quaternion initialRotation;

    void Start()
    {
        xRing = CreateRing("X Ring", xAxisColor, Vector3.right);
        yRing = CreateRing("Y Ring", yAxisColor, Vector3.up);
        zRing = CreateRing("Z Ring", zAxisColor, Vector3.forward);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectSelectedRing();
        }

        if (Input.GetMouseButton(0) && selectedRing != null)
        {
            RotateObjectWithMouse();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedRing = null;
        }
    }

    private LineRenderer CreateRing(string name, Color color, Vector3 axis)
    {
        GameObject ringObject = new GameObject(name);
        ringObject.transform.parent = transform;
        ringObject.transform.localPosition = Vector3.zero;

        LineRenderer lineRenderer = ringObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = ringThickness;
        lineRenderer.endWidth = ringThickness;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = color };

        // Generate the ring mesh and assign the mesh collider
        Mesh ringMesh = CreateRingMesh();
        MeshCollider meshCollider = ringObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = ringMesh;
        meshCollider.convex = false;  // Set it to concave

        // Create points for the ring
        SetRingPositions(lineRenderer, axis);

        return lineRenderer;
    }

    private Mesh CreateRingMesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        float angleStep = 360f / segments;

        // Create vertices for the ring (a hollow ring, with an inner and outer radius)
        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleStep;
            float xOuter = Mathf.Cos(Mathf.Deg2Rad * angle) * ringRadius;
            float yOuter = Mathf.Sin(Mathf.Deg2Rad * angle) * ringRadius;

            float xInner = Mathf.Cos(Mathf.Deg2Rad * angle) * (ringRadius - ringThickness);
            float yInner = Mathf.Sin(Mathf.Deg2Rad * angle) * (ringRadius - ringThickness);

            vertices.Add(new Vector3(xOuter, yOuter, 0)); // Outer ring
            vertices.Add(new Vector3(xInner, yInner, 0)); // Inner ring

            if (i < segments - 1)
            {
                int indexOuter1 = i * 2;
                int indexOuter2 = indexOuter1 + 1;
                int indexOuter3 = (i + 1) % segments * 2;
                int indexOuter4 = indexOuter3 + 1;

                // Create triangles for the outer ring
                triangles.Add(indexOuter1);
                triangles.Add(indexOuter3);
                triangles.Add(indexOuter2);
                triangles.Add(indexOuter2);
                triangles.Add(indexOuter3);
                triangles.Add(indexOuter4);
            }
            else
            {
                // Connect the last to the first to close the loop
                int indexOuter1 = (segments - 1) * 2;
                int indexOuter2 = indexOuter1 + 1;
                int indexOuter3 = 0;
                int indexOuter4 = 1;

                // Create triangles for the outer ring
                triangles.Add(indexOuter1);
                triangles.Add(indexOuter3);
                triangles.Add(indexOuter2);
                triangles.Add(indexOuter2);
                triangles.Add(indexOuter3);
                triangles.Add(indexOuter4);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    private void SetRingPositions(LineRenderer lineRenderer, Vector3 axis)
    {
        int numSegments = 100;
        float angle = 0f;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= numSegments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * ringRadius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * ringRadius;

            if (axis == Vector3.right)
                points.Add(transform.position + transform.rotation * new Vector3(0, x, y));
            else if (axis == Vector3.up)
                points.Add(transform.position + transform.rotation * new Vector3(x, 0, y));
            else if (axis == Vector3.forward)
                points.Add(transform.position + transform.rotation * new Vector3(x, y, 0));

            angle += 360f / numSegments;
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    private void DetectSelectedRing()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == xRing.transform)
            {
                selectedRing = xRing.transform;
                rotationAxis = transform.right;
            }
            else if (hit.transform == yRing.transform)
            {
                selectedRing = yRing.transform;
                rotationAxis = transform.up;
            }
            else if (hit.transform == zRing.transform)
            {
                selectedRing = zRing.transform;
                rotationAxis = transform.forward;
            }

            if (selectedRing != null)
            {
                initialRotation = transform.rotation;
                initialMouseDirection = GetMouseDirectionOnPlane(rotationAxis);
            }
        }
    }

    private void RotateObjectWithMouse()
    {
        Vector3 currentMouseDirection = GetMouseDirectionOnPlane(rotationAxis);
        float angle = Vector3.SignedAngle(initialMouseDirection, currentMouseDirection, rotationAxis);

        transform.rotation = initialRotation * Quaternion.AngleAxis(angle, rotationAxis);
    }

    private Vector3 GetMouseDirectionOnPlane(Vector3 planeNormal)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane rotationPlane = new Plane(planeNormal, transform.position);

        if (rotationPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            return (hitPoint - transform.position).normalized;
        }

        return Vector3.zero;
    }
}
