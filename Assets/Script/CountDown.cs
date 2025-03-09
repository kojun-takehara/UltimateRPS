using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public Text timerText;             // UI Text to display the timer
    public Text finishText;            // UI Text to display the finish message
    public Canvas currentCanvas;       // Canvas to disable after countdown
    public Canvas targetCanvas;        // Canvas to enable after countdown
    private CountUpToValueZ countUpToValueZ; // Reference to CountUpToValueZ script
    private BoneRotationValueManager boneRotationValueManager; // Reference to BoneRotationValueManager

    private float countdownTime = 20f; // Set countdown starting time
    private bool isCountingDown = false; // Track if the timer is running

    void Start()
    {
        // Ensure finish text is hidden at the start
        finishText.gameObject.SetActive(false);
    }

    // This method is triggered by the button click
    public void StartCountdown()
    {
        if (!isCountingDown) // Prevent multiple countdowns at the same time
        {
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second

        isCountingDown = true;
        float timeRemaining = countdownTime;

        // Countdown loop
        while (timeRemaining > 0)
        {
            timerText.text = "残り時間: " + Mathf.Ceil(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        // Display end message
        timerText.text = "";
        finishText.text = "終了！！！";
        finishText.gameObject.SetActive(true);  // Enable finish text when countdown ends
        
        yield return new WaitForSeconds(1f); // Wait for a moment to show the finish text

        // Disable current canvas and enable target canvas
        currentCanvas.gameObject.SetActive(false);
        targetCanvas.gameObject.SetActive(true);

        // Delay before calculating the bone rotations
        float delayTime = 0.5f; // Delay time before calculation, adjust if needed
        yield return new WaitForSeconds(delayTime); // Delay before calling CalculateRotations

        // Trigger rotation calculation in BoneRotationSum after countdown ends
        if (BoneRotationSum.Instance != null)
        {
            BoneRotationSum.Instance.CalculateRotations(); // Call method to calculate rotations
        }

        // Trigger rotation counting for other actions (e.g., StartCountingUpToRemainderZ)
        if (countUpToValueZ != null)
        {
            countUpToValueZ.StartCountingUpToRemainderZ(); // Assuming this method is defined elsewhere
        }

        // Find the DeleteFingerJointChildren script in the new scene
        DeleteFingerJoint fingerJointScript = FindObjectOfType<DeleteFingerJoint>();
        if (fingerJointScript != null)
        {
            fingerJointScript.DeleteFingerJointChildren();
        }

        isCountingDown = false;
    }

    // Link CountUpToValueZ script to the CountDown script
    public void SetCountUpToValueZ(CountUpToValueZ countUpToValueZScript)
    {
        countUpToValueZ = countUpToValueZScript;
    }

    // Link BoneRotationValueManager script to the CountDown script
    public void SetBoneRotationValueManager(BoneRotationValueManager boneRotationValueManagerScript)
    {
        boneRotationValueManager = boneRotationValueManagerScript;
    }
}
