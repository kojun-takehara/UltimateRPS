using UnityEngine;

public class DeleteFingerJoint : MonoBehaviour
{
    public void DeleteFingerJointChildren()
    {
        Debug.Log("Deleting FingerJoint tagged objects...");
        DeleteRecursively(transform);
    }

    private void DeleteRecursively(Transform parent)
    {
        // Iterate through all direct children
        foreach (Transform child in parent)
        {
            // Check if the child has the "FingerJoint" tag
            if (child.CompareTag("FingerJoint"))
            {
                Destroy(child.gameObject); // Delete the object
            }
            else
            {
                // Recursively call DeleteRecursively on children
                DeleteRecursively(child);
            }
        }
    }
}
