using UnityEngine;

public class BoneRotationSum : MonoBehaviour
{
    public static BoneRotationSum Instance;  // Singleton instance
    public BoneRotationValueManager boneRotationValueManager;
    private float remainderX;
    private float remainderZ;

    void Awake()
    {
        // If there is already an instance, destroy this one to maintain the singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Set this instance
        Instance = this;

        // Make sure the object persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Removed the initial calculation here, we'll trigger it later
    }

    // This method will be called by CountDown when countdown finishes
    public void CalculateRotations()
    {
        // Calculate the sum of x and z rotations immediately after countdown ends
        float sumX = 0f;
        float sumZ = 0f;

        foreach (var boneRotation in boneRotationValueManager.BoneRotations.Values)
        {
            sumX += -boneRotation.eulerAngles.x;
            sumZ += -boneRotation.eulerAngles.z;
        }

        sumX = Mathf.Round(sumX / 10f) * 10f;
        sumZ = Mathf.Round(sumZ / 10f) * 10f;

        remainderX = Mathf.Abs(2200f + sumX);
        remainderZ = Mathf.Abs(6200f + sumZ);

        Debug.Log("芸術点: " + remainderX + " 万点");
        Debug.Log("戦闘力: " + remainderZ + " 万点");
    }

    // Get the remainderX value
    public float GetRemainderX()
    {
        return remainderX;
    }

    // Get the remainderZ value
    public float GetRemainderZ()
    {
        return remainderZ;
    }
}
