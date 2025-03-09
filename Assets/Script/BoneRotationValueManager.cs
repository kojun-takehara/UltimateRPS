using UnityEngine;
using System.Collections.Generic;

public class BoneRotationValueManager : MonoBehaviour
{
    private Dictionary<string, Quaternion> boneRotations = new Dictionary<string, Quaternion>();
    
    // Public property to access bone rotation values without allowing modification
    public IReadOnlyDictionary<string, Quaternion> BoneRotations => boneRotations;

    void Update()
    {
        // This part is removed because you no longer want it to trigger on Space key press
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Get rotations of all bones and store in the dictionary
            GetAllBoneRotations(transform);
        }
        */

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Log all stored bone rotations
            foreach (var bone in boneRotations)
            {
                Debug.Log($"Bone: {bone.Key}, Rotation: {bone.Value}");
            }
        }
    }

    // This method will now be called from another script after countdown hits 0
    public void GetRotationsAfterCountdown()
    {
        // Get rotations of all bones and store in the dictionary
        GetAllBoneRotations(transform);
    }

    private void GetAllBoneRotations(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.name.StartsWith("Bone."))
            {
                boneRotations[child.name] = child.rotation;
            }
            
            // Recursively get rotations of all child bones
            GetAllBoneRotations(child);
        }
    }
}
