/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script handles the interaction with evidence objects in the game. The player can search for evidence, 
 * and upon finding all pieces, it unlocks a linked door. It also manages the player's camera and movement during evidence interaction.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the interaction with evidence objects, including searching for evidence, switching cameras, and unlocking a linked door.
/// </summary>
public class Evidence : Interactable
{
    /// <summary>
    /// Reference to the player's camera.
    /// </summary>
    [SerializeField]
    private GameObject playerCam;

    /// <summary>
    /// Reference to the camera used during evidence interaction.
    /// </summary>
    [SerializeField]
    private GameObject evidenceCam;

    /// <summary>
    /// Reference to the player movement script to enable/disable movement.
    /// </summary>
    [SerializeField]
    private MonoBehaviour playerMovementScript;

    /// <summary>
    /// Reference to the evidence buttons UI.
    /// </summary>
    [SerializeField]
    private GameObject evidenceButtons;

    /// <summary>
    /// Reference to the player interacting with the evidence.
    /// </summary>
    public Player player;

    /// <summary>
    /// Reference to the NPC involved with the evidence.
    /// </summary>
    public NPC npc;

    /// <summary>
    /// Reference to the door linked to the evidence, which gets unlocked after all evidence is found.
    /// </summary>
    public Door linkedDoor;

    /// <summary>
    /// Flag indicating whether the player is currently searching for evidence.
    /// </summary>
    public bool searching = false;

    /// <summary>
    /// Counter for the first piece of evidence.
    /// </summary>
    float evidenceOne = 0;

    /// <summary>
    /// Counter for the second piece of evidence.
    /// </summary>
    float evidenceTwo = 0;

    /// <summary>
    /// Counter for the third piece of evidence.
    /// </summary>
    float evidenceThree = 0;

    /// <summary>
    /// Flag indicating if the first piece of evidence is completed.
    /// </summary>
    private bool evidenceOneDone = false;

    /// <summary>
    /// Flag indicating if the second piece of evidence is completed.
    /// </summary>
	private bool evidenceTwoDone = false;

    /// <summary>
    /// Flag indicating if the third piece of evidence is completed.
    /// </summary>
	private bool evidenceThreeDone = false;

    /// <summary>
    /// Handles the interaction when the player interacts with the evidence.
    /// </summary>
    /// <param name="thePlayer">The player that interacted with the evidence.</param>
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
            player.SetEvidence(this);

            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            base.Interact(thePlayer);
            player.ExitText();
            Debug.Log("run");
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
            Debug.Log("run");
            player.ResetEvidence();
        }
    }

    /// <summary>
    /// Coroutine to show evidence buttons after a delay during evidence interaction.
    /// </summary>
    /// <returns>IEnumerator for coroutine.</returns>
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

    /// <summary>
    /// Handles interaction with the first piece of evidence.
    /// </summary>
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

    /// <summary>
    /// Handles interaction with the second piece of evidence.
    /// </summary>
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

    /// <summary>
    /// Handles interaction with the third piece of evidence.
    /// </summary>
    public void EvidenceThree()
    {
        GameManager.instance.EvidenceThreeCheck();
        evidenceThreeDone = true;
		Debug.Log("ThreeDone");
        EvidenceDone();
    }

    /// <summary>
    /// Checks if all pieces of evidence are completed and unlocks the linked door.
    /// </summary>
    private void EvidenceDone()
    {
        if (evidenceOneDone && evidenceTwoDone && evidenceThreeDone)
        {
            linkedDoor.UnlockOutsideDoor();
        }
    }

    /// <summary>
    /// Handles interaction with bank statements as evidence.
    /// </summary>
    public void BankStatements()
    {
        npc.EvidenceTwoDone();
    }
}

