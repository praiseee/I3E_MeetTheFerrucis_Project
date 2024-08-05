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

    bool searching = false;

    float evidenceOne = 0;
    float evidenceTwo = 0;
	
    public override void Interact(Player thePlayer)
    {
        if (!searching)
        {
            searching = true;
            evidenceCam.gameObject.SetActive(true);
            playerCam.gameObject.SetActive(false);
            StartCoroutine(ShowEvidenceButtonsWithDelay());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            base.Interact(thePlayer);
        }
        else
        {
            searching = false;
            playerCam.gameObject.SetActive(true);
            evidenceCam.gameObject.SetActive(false);
            evidenceButtons.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Enable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true;
            }
        }
    }

    private IEnumerator ShowEvidenceButtonsWithDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2.0f);

        // Show the evidence buttons
        evidenceButtons.gameObject.SetActive(true);
    }

    public void EvidenceOne()
    {
        evidenceOne = evidenceOne + 1;
        if(evidenceOne == 3)
        {
            GameManager.instance.EvidenceOneCheck();
        }
    }

    public void EvidenceTwo()
    {
        evidenceTwo = evidenceTwo + 1;
        if(evidenceTwo == 2)
        {
            GameManager.instance.EvidenceTwoCheck();
        }
    }
}