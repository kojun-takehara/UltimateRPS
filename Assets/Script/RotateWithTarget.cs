using UnityEngine;

public class SmoothRotateWithTargetWithLimits : MonoBehaviour
{
    public Transform rotationSource;

    public Vector2 xRotationLimits = new Vector2(-45f, 45f);
    public Vector2 yRotationLimits = new Vector2(-30f, 30f);
    public Vector2 zRotationLimits = new Vector2(-60f, 60f);

    public float rotationSmoothSpeed = 5f;

    private Quaternion initialLocalRotation;

    void Start()
    {
        // Store the initial local rotation
        initialLocalRotation = transform.localRotation;
    }

    void Update()
    {
        if (rotationSource != null)
        {
            // Calculate the target rotation relative to the initial rotation
            Quaternion targetLocalRotation = Quaternion.Inverse(rotationSource.parent.rotation) * rotationSource.rotation;
            Vector3 targetEulerAngles = targetLocalRotation.eulerAngles;

            // Correct angles to range -180 to 180
            targetEulerAngles.x = targetEulerAngles.x > 180 ? targetEulerAngles.x - 360 : targetEulerAngles.x;
            targetEulerAngles.y = targetEulerAngles.y > 180 ? targetEulerAngles.y - 360 : targetEulerAngles.y;
            targetEulerAngles.z = targetEulerAngles.z > 180 ? targetEulerAngles.z - 360 : targetEulerAngles.z;

            // Apply limits relative to the initial rotation
            targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, xRotationLimits.x, xRotationLimits.y);
            targetEulerAngles.y = Mathf.Clamp(targetEulerAngles.y, yRotationLimits.x, yRotationLimits.y);
            targetEulerAngles.z = Mathf.Clamp(targetEulerAngles.z, zRotationLimits.x, zRotationLimits.y);

            // Apply the rotation back to the transform, considering initial rotation
            Quaternion limitedLocalRotation = Quaternion.Euler(targetEulerAngles) * initialLocalRotation;

            // Smoothly interpolate to the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, limitedLocalRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}
