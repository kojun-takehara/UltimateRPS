using UnityEngine;
using System.Collections;
public class CountUpToValueZ : MonoBehaviour
{
    private BoneRotationSum boneRotationSum; // Reference to BoneRotationSum script
    private float remainderZ;
    private float currentValue = 0f;

    void Start()
    {
        // Find BoneRotationSum in the current scene (it should be persistent)
        boneRotationSum = FindObjectOfType<BoneRotationSum>();

        if (boneRotationSum == null)
        {
            Debug.LogError("BoneRotationSum reference not found.");
        }
    }

    // This method will be called when the countdown hits 0
    public void StartCountingUpToRemainderZ() // Ensure this method exists
    {
        // Retrieve the remainderZ value from BoneRotationSum
        if (boneRotationSum != null)
        {
            remainderZ = boneRotationSum.GetRemainderZ();
            StartCoroutine(CountUpToRemainderZ());
        }
    }

    private IEnumerator CountUpToRemainderZ()
    {
        while (currentValue < remainderZ)
        {
            currentValue += Time.deltaTime * 10f; // Adjust speed as needed
            Debug.Log("Counting Up: " + currentValue);
            yield return null;
        }

        currentValue = remainderZ; // Ensure it reaches exactly the remainderZ
        Debug.Log("Final Value Reached: " + currentValue);
    }
}
