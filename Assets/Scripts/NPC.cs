/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script handles NPC interactions, dialogue management, and the conditions required for progressing through the game. 
 * It controls NPC movement, dialogue sequences, and camera behavior during player interactions.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages NPC interactions, dialogue sequences, and movement during player interactions.
/// Controls specific dialogues based on game conditions and handles camera adjustments during interactions.
/// </summary>
public class NPC : Interactable
{
    private bool talking = false; // Indicates if the NPC is currently talking to the player.
    public Player player; // Reference to the player interacting with the NPC.

    [SerializeField]
    private MonoBehaviour playerMovementScript; // Reference to the player's movement script.
    private Quaternion originalRotation; // Stores the NPC's original rotation for smooth return after interaction.

    [SerializeField]
    private float rotationSpeed = 2.0f; // Speed at which the NPC rotates to face the player.

    public reactiveAI npcMovement; // Reference to the NPC's movement AI script.
    public Animator npcAnimator; // Reference to the NPC's Animator component.

    // NPC Identification
    public bool sister = false;
    public bool brother = false;
    public bool father = false;
    public bool grandfather = false;
    public bool chief = false;

    // Sister Dialogue
    public Dialogue sisterStarting;
    public Dialogue sisterIdle;
    public Dialogue sisterArgue;

    // Brother Dialogue
    public Dialogue brotherStarting;
    public Dialogue brotherIdle;
    public Dialogue brotherArgue;
    public Dialogue brotherKiller;
    public Dialogue brotherEnd;

    // Father Dialogue
    public Dialogue fatherStarting;
    public Dialogue fatherIdle;
    public Dialogue fatherIdle2;
    public Dialogue fatherBank;
    public Dialogue fatherShoe;
    public Dialogue fatherFinal;
    public Dialogue fatherDone;

    // Grandfather Dialogue
    public Dialogue grandfatherIdle;
    public Dialogue grandfatherKiller;
    public Dialogue grandfatherFollow;

    // Player Dialogue
    public Dialogue talkToFatherFirstSister;
    public Dialogue talkToFatherFirstBrother;

    // Chief Dialogue
    public Dialogue chiefDialogue;

    // Game State Conditions
    private static bool argueClueFound = false;
    private static bool sisterTwoDone = false;
    private static bool brotherOneDone = false;
    private static bool brotherDone = false;
    private static bool fatherOneDone = false;
    private static bool fatherTwoDone = false;
    private static bool fatherThreeDone = false;
    private static bool fatherFourDone = false;
    private static bool grandfatherOneDone = false;
    private static bool bankStatementsFound = false;
    private static bool shoeFound = false;
    private static bool motherLeave = false;
    private static bool killerOne = false;
    private static bool killerTwo = false;
    private float murderWeapon = 0;

    /// <summary>
    /// Stores the original rotation of the NPC upon awakening.
    /// </summary>
    private void Awake()
    {
        // Uncomment this line if npcMovement is not assigned in the inspector
        // npcMovement = GetComponent<reactiveAI>();
    }

    /// <summary>
    /// Interacts with the NPC, initiating dialogue, stopping NPC movement and animation, and adjusting the camera.
    /// </summary>
    /// <param name="thePlayer">The player interacting with the NPC.</param>
    public override void Interact(Player thePlayer)
    {
        if (!talking)
        {
            talking = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Stop NPC movement and animation if applicable
            if (father)
            {
                npcMovement.StopMoving();
            }

            if (npcAnimator != null && !npcMovement.destinationReached)
            {
                npcAnimator.SetTrigger("IsTrigger");
            }

            // Store the original rotation if it's the first time talking
            if (!IsInvoking("StoreOriginalRotation"))
            {
                StoreOriginalRotation();
            }

            // Start coroutine to smoothly rotate towards the player
            StopAllCoroutines();
            StartCoroutine(SmoothLookAt(player.transform));

            StartDialogue();

            // Disable player movement during interaction
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            base.Interact(thePlayer);
        }
    }

    /// <summary>
    * Starts the appropriate dialogue sequence based on the current game conditions and the NPC's role.
    * </summary>
    public void StartDialogue()
    {
        // Dialogue handling for the sister NPC
        if (sister)
        {
            if (!argueClueFound)
            {
                if (!fatherOneDone)
                {
                    talkToFatherFirstSister.RunDialogue();
                    player.SetDialogue(talkToFatherFirstSister);
                }
                else
                {
                    sisterStarting.RunDialogue();
                    player.SetDialogue(sisterStarting);
                }
            }
            else if (fatherThreeDone)
            {
                if (!sisterTwoDone)
                {
                    sisterArgue.RunDialogue();
                    player.SetDialogue(sisterArgue);
                }
                else
                {
                    sisterIdle.RunDialogue();
                    player.SetDialogue(sisterIdle);
                }
            }
            else
            {
                sisterIdle.RunDialogue();
                player.SetDialogue(sisterIdle);
            }
        }

        // Dialogue handling for the brother NPC
        if (brother)
        {
            if (!brotherOneDone)
            {
                if (!fatherOneDone)
                {
                    talkToFatherFirstBrother.RunDialogue();
                    player.SetDialogue(talkToFatherFirstBrother);
                }
                else
                {
                    brotherStarting.RunDialogue();
                    player.SetDialogue(brotherStarting);
                }
            }
            else if (fatherThreeDone && !killerOne)
            {
                if (!motherLeave)
                {
                    brotherArgue.RunDialogue();
                    player.SetDialogue(brotherArgue);
                }
                else
                {
                    brotherIdle.RunDialogue();
                    player.SetDialogue(brotherIdle);
                }
            }
            else if (killerOne)
            {
                if (!brotherDone)
                {
                    brotherKiller.RunDialogue();
                    player.SetDialogue(brotherKiller);
                }
                else
                {
                    brotherEnd.RunDialogue();
                    player.SetDialogue(brotherEnd);
                }
            }
            else
            {
                brotherIdle.RunDialogue();
                player.SetDialogue(brotherIdle);
            }
        }

        // Dialogue handling for the father NPC
        if (father)
        {
            if (argueClueFound && !bankStatementsFound && !motherLeave)
            {
                if (!fatherTwoDone)
                {
                    fatherIdle2.RunDialogue();
                    player.SetDialogue(fatherIdle2);
                }
                else
                {
                    fatherIdle.RunDialogue();
                    player.SetDialogue(fatherIdle);
                }
            }
            else if (!fatherOneDone)
            {
                fatherStarting.RunDialogue();
                player.SetDialogue(fatherStarting);
            }
            else if (bankStatementsFound && !motherLeave)
            {
                if (!fatherThreeDone)
                {
                    fatherBank.RunDialogue();
                    player.SetDialogue(fatherBank);
                }
                else
                {
                    fatherIdle.RunDialogue();
                    player.SetDialogue(fatherIdle);
                }
            }
            else if (motherLeave && !shoeFound)
            {
                if (!fatherFourDone)
                {
                    fatherShoe.RunDialogue();
                    player.SetDialogue(fatherShoe);
                }
                else
                {
                    fatherIdle.RunDialogue();
                    player.SetDialogue(fatherIdle);
                }
            }
            else if (shoeFound)
            {
                if (!killerOne)
                {
                    fatherFinal.RunDialogue();
                    player.SetDialogue(fatherFinal);
                }
                else
                {
                    fatherDone.RunDialogue();
                    player.SetDialogue(fatherDone);
                }
            }
            else
            {
                fatherIdle.RunDialogue();
                player.SetDialogue(fatherIdle);
            }
        }

        // Dialogue handling for the grandfather NPC
        if (grandfather)
        {
            if (brotherDone && !killerTwo)
            {
                if (!grandfatherOneDone)
                {
                    grandfatherKiller.RunDialogue();
                    player.SetDialogue(grandfatherKiller);
                }
                else
                {
                    grandfatherIdle.RunDialogue();
                    player.SetDialogue(grandfatherIdle);
                }
            }
            else if (killerTwo)
            {
                grandfatherFollow.RunDialogue();
                player.SetDialogue(grandfatherFollow);
            }
            else
            {
                grandfatherIdle.RunDialogue();
                player.SetDialogue(grandfatherIdle);
            }
        }

        // Dialogue handling for the chief NPC
        if (chief)
        {
            chiefDialogue.RunDialogue();
            player.SetDialogue(chiefDialogue);
        }
    }

    /// <summary>
    /// Stores the NPC's original rotation to allow smooth return after interaction.
    /// </summary>
    private void StoreOriginalRotation()
    {
        originalRotation = transform.rotation;
    }

    /// <summary>
    /// Ends the dialogue, resets NPC and player states, and returns the NPC to its original rotation.
    /// </summary>
    public void ExitDialogue()
    {
        talking = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Smoothly return NPC to original rotation
        StopAllCoroutines();
        StartCoroutine(SmoothReturnToOriginalRotation());

        // Resume NPC movement and animation if applicable
        if (father)
        {
            npcMovement.ResumeMoving();
        }

        if (npcAnimator != null)
        {
            npcAnimator.SetTrigger("IsTrigger");
        }

        // Re-enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }

    /// <summary>
    * Coroutine to smoothly rotate the NPC to face the target (player) during interaction.
    * </summary>
    /// <param name="target">The target to look at (usually the player).</param>
    /// <returns>An IEnumerator for the coroutine.</returns>
    private IEnumerator SmoothLookAt(Transform target)
    {
        if (brother)
        {
            yield break;
        }
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);

        // Smoothly rotate to look at the target
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    /// <summary>
    * Coroutine to smoothly rotate the NPC back to its original rotation after interaction.
    * </summary>
    /// <returns>An IEnumerator for the coroutine.</returns>
    private IEnumerator SmoothReturnToOriginalRotation()
    {
        // Smoothly rotate back to the original rotation
        while (Quaternion.Angle(transform.rotation, originalRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.rotation = originalRotation;
    }

    // Methods to set various game conditions and trigger inventory updates

    /// <summary>
    * Sets the condition that the argue clue has been found and updates the inventory.
    * </summary>
    public void EvidenceOneDone()
    {
        argueClueFound = true;
        GameManager.instance.ArguementInventory();
        GameManager.instance.evidenceObjective.text = "Talk to the Dad";
    }

    /// <summary>
    * Sets the condition that the sister's second dialogue has been completed.
    * </summary>
    public void SisterTwoDone()
    {
        sisterTwoDone = true;
    }

    /// <summary>
    * Sets the condition that the brother's first dialogue has been completed.
    * </summary>
    public void BrotherOneDone()
    {
        brotherOneDone = true;
    }

    /// <summary>
    * Sets the condition that the brother's dialogues are fully completed and updates the inventory.
    * </summary>
    public void BrotherDone()
    {
        brotherDone = true;
        GameManager.instance.KeyInventory();
        GameManager.instance.evidenceObjective.text = "Talk to the Grandfather";
    }

    /// <summary>
    * Sets the condition that the father's first dialogue has been completed.
    * </summary>
    public void FatherOneDone()
    {
        fatherOneDone = true;
        GameManager.instance.evidenceObjective.text = "Talk to the Kids";
    }

    /// <summary>
    * Sets the condition that the father's second dialogue has been completed.
    * </summary>
    public void FatherTwoDone()
    {
        fatherTwoDone = true;
        GameManager.instance.evidenceObjective.text = "Search the house for clues";
    }

    /// <summary>
    * Sets the condition that the father's third dialogue has been completed.
    * </summary>
    public void FatherThreeDone()
    {
        fatherThreeDone = true;
        brotherOneDone = true;
        GameManager.instance.evidenceObjective.text = "Talk to the Kids";
    }

    /// <summary>
    * Sets the condition that the father's fourth dialogue has been completed.
    * </summary>
    public void FatherFourDone()
    {
        fatherFourDone = true;
        GameManager.instance.evidenceObjective.text = "Investigate the Bathtub";
    }

    /// <summary>
    * Sets the condition that the grandfather's first dialogue has been completed.
    * </summary>
    public void GrandfatherOneDone()
    {
        grandfatherOneDone = true;
        GameManager.instance.evidenceObjective.text = "Search the Shelf";
    }

    /// <summary>
    * Sets the condition that the bank statements have been found and updates the inventory.
    * </summary>
    public void EvidenceTwoDone()
    {
        bankStatementsFound = true;
        GameManager.instance.BankStatementInventory();
        GameManager.instance.evidenceObjective.text = "Talk to the Father";
    }

    /// <summary>
    * Sets the condition that the mother has left and updates the inventory.
    * </summary>
    public void EvidenceThreeDone()
    {
        motherLeave = true;
        GameManager.instance.VictimLeavingInventory();
        GameManager.instance.evidenceObjective.text = "Find the Father";
    }

    /// <summary>
    * Sets the condition that the shoe has been found and updates the inventory.
    * </summary>
    public void EvidenceFourDone()
    {
        shoeFound = true;
        GameManager.instance.ShoeInventory();
        GameManager.instance.evidenceObjective.text = "Talk to the Father";
    }

    /// <summary>
    * Sets the condition that the first killer (father) has been identified and updates the inventory.
    * </summary>
    public void KillerOne()
    {
        killerOne = true;
        GameManager.instance.DadProtectInventory();
        GameManager.instance.evidenceObjective.text = "Arrest the Brother";
        npcAnimator.SetTrigger("IsTrigger");
    }

    /// <summary>
    * Sets the condition that the second killer (grandfather) has been identified and updates the inventory.
    * </summary>
    public void KillerTwo()
    {
        murderWeapon += 1;
        if (murderWeapon == 2)
        {
            killerTwo = true;
            GameManager.instance.evidenceObjective.text = "Arrest the Grandfather";
        }
    }
}




