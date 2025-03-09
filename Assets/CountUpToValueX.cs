using UnityEngine;
using System.Collections; // Add this to use IEnumerator

public class CountUpToValueX : MonoBehaviour
{
    private BoneRotationSum boneRotationSum; 
    private CountDown countDown;
    private float remainderX;

    void Start()
    {
        // Find BoneRotationSum in the current scene (it should be persistent)
        boneRotationSum = FindObjectOfType<BoneRotationSum>();

        if (boneRotationSum == null)
        {
            Debug.LogError("BoneRotationSum reference not found.");
        }
    }

    void Update()
    {
        // Retrieve the remainderX value from BoneRotationSum
        if (boneRotationSum != null)
        {
            remainderX = boneRotationSum.GetRemainderX();
            
        }
    }
}
