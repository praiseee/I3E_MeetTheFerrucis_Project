using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : Interactable
{
    [SerializeField]
    private GameObject playerCam;

    [SerializeField]
    private GameObject evidenceCam;

    // Reference to the player movement script
    [SerializeField]
    private MonoBehaviour playerMovementScript;

    [SerializeField]
    private GameObject evidenceButtons;

    public Player player;

    public Door linkedDoor;

    public bool searching = false;

    float evidenceOne = 0;
    float evidenceTwo = 0;
    float evidenceThree = 0;

    private bool evidenceOneDone = false;
	private bool evidenceTwoDone = false;
	private bool evidenceThreeDone = false;

    public override void Interact(Player thePlayer)
    {
        if (!searching)
        {
            searching = true;
            evidenceCam.SetActive(true);
            playerCam.SetActive(false);
            StartCoroutine(ShowEvidenceButtonsWithDelay());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            base.Interact(thePlayer);
            player.ExitText();
        }
        else
        {
            searching = false;
            playerCam.SetActive(true);
            evidenceCam.SetActive(false);
            evidenceButtons.SetActive(false); // Hide buttons when quitting evidence interaction
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Enable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true;
            }
            player.ExitText();
        }
    }

    private IEnumerator ShowEvidenceButtonsWithDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // Show the evidence buttons if still searching
        if (searching)
        {
            evidenceButtons.SetActive(true);
        }
    }

    public void EvidenceOne()
    {
        evidenceOne += 1;
        if (evidenceOne == 3)
        {
            GameManager.instance.EvidenceOneCheck();
            evidenceOneDone = true;
		    Debug.Log("OneDone");
            EvidenceDone();
        }
    }

    public void EvidenceTwo()
    {
        evidenceTwo += 1;
        if (evidenceTwo == 2)
        {
            GameManager.instance.EvidenceTwoCheck();
            evidenceTwoDone = true;
		    Debug.Log("TwoDone");
            EvidenceDone();
        }
    }

    public void EvidenceThree()
    {
        GameManager.instance.EvidenceThreeCheck();
        evidenceThreeDone = true;
		Debug.Log("ThreeDone");
        EvidenceDone();
    }

    private void EvidenceDone()
    {
        if(evidenceOneDone && evidenceTwoDone && evidenceThreeDone)
        {
            linkedDoor.UnlockOutsideDoor();
        }
    }
}
