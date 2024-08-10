/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * The door that opens when the player is near it and presses the interact button.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Handles door interactions, including opening, closing, locking, and scene transitions.
/// </summary>
public class Door : Interactable
{
    /// <summary>
    /// Text to display when the door is locked.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI lockedDoorText;

    /// <summary>
    /// Audio source for the interaction sound.
    /// </summary>
    [SerializeField]
    private AudioSource interactionSound;

    /// <summary>
    /// Audio source for the locked door sound.
    /// </summary>
    [SerializeField]
    private AudioSource lockedSound;

    /// <summary>
    /// Dialogue to run when the player interacts with the outside door.
    /// </summary>
    public Dialogue outsideDoorDialogue;

    /// <summary>
    /// Dialogue to run when the player interacts with the inside door.
    /// </summary>
    public Dialogue insideDoorDialogue;

    /// <summary>
    /// Reference to the player.
    /// </summary>
    public Player player;

    /// <summary>
    /// Indicates whether the door is currently open.
    /// </summary>
    bool opened = false;

    /// <summary>
    /// Indicates if this door is an outside door.
    /// </summary>
    public bool outsideDoor = false;

    /// <summary>
    /// Indicates if this door is an inside door.
    /// </summary>
    public bool insideDoor = false;

    /// <summary>
    /// Indicates if this door triggers a scene change.
    /// </summary>
    public bool sceneChanger = false;

    /// <summary>
    /// Reference to the LevelLoader for handling scene transitions.
    /// </summary>
    public LevelLoader levelLoader;

    /// <summary>
    /// Indicates if the outside door is locked.
    /// </summary>
    bool outsideDoorLocked = true;

    /// <summary>
    /// Indicates if the inside door is locked.
    /// </summary>
    bool insideDoorLocked = true;

    /// <summary>
    /// Indicates if the door is locked.
    /// </summary>
    public bool locked = false;
    
    /// <summary>
    /// Indicates if the player is near the door.
    /// </summary>
    bool playerNear = false;

    /// <summary>
    /// Indicates if an NPC is near the door.
    /// </summary>
    bool npcNear = false;

    /// <summary>
    /// Unlocks the outside door.
    /// </summary>
    public void UnlockOutsideDoor()
    {
        outsideDoorLocked = false;
    }

    /// <summary>
    /// Unlocks the inside door.
    /// </summary>
    public void UnlockInsideDoor()
    {
        insideDoorLocked = false;
    }

    /// <summary>
    /// Handles the door's interaction when the player presses the interact button.
    /// </summary>
    /// <param name="thePlayer">The player that interacted with the door.</param>
    public override void Interact(Player thePlayer)
    {
        base.Interact(thePlayer);
        OpenDoor();
    }

    /// <summary>
    /// Handles the logic for opening the door.
    /// </summary>
    public void OpenDoor()
    {
        if (outsideDoor && outsideDoorLocked)
        {
            outsideDoorDialogue.RunDialogue();
            player.SetDialogue(outsideDoorDialogue);
            return;
        }

        if (insideDoor && insideDoorLocked)
        {
            insideDoorDialogue.RunDialogue();
            player.SetDialogue(insideDoorDialogue);
            return;
        }

        if (locked && !insideDoor && !outsideDoor)
        {
            lockedSound.Play();
        }

        if (!locked && !opened)
        {
            interactionSound.Play();

            // Create a new Vector3 and store the current rotation.
            Vector3 newRotation = transform.eulerAngles;

            // Add 90 degrees to the y axis rotation
            newRotation.y += 90f;

            // Assign the new rotation to the transform
            transform.eulerAngles = newRotation;

            // Set the opened bool to true
            opened = true;
        }

        if (sceneChanger)
        {
            levelLoader.SwitchScene();

            if (outsideDoor)
            {
                GameManager.instance.evidenceObjective.text = "Talk to the Dad";
            }
            if (insideDoor)
            {
                GameManager.instance.evidenceObjective.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Handles the logic for closing the door.
    /// </summary>
    public void CloseDoor()
    {
        if (!locked && opened)
        {
            // Create a new Vector3 and store the current rotation.
            Vector3 newRotation = transform.eulerAngles;

            // Subtract 90 degrees from the y axis rotation
            newRotation.y -= 90f;

            // Assign the new rotation to the transform
            transform.eulerAngles = newRotation;

            // Set the opened bool to false
            opened = false;
        }
    }

    /// <summary>
    /// Sets the lock status of the door.
    /// </summary>
    /// <param name="lockStatus">The lock status of the door.</param>
    public void SetLock(bool lockStatus)
    {
        locked = lockStatus;
    }

    /// <summary>
    /// Sets the status of the player being near the door and handles closing the door if necessary.
    /// </summary>
    /// <param name="isNear">Is the player near the door.</param>
    public void SetPlayerNear(bool isNear)
    {
        playerNear = isNear;

        if (!playerNear && !npcNear)
        {
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    /// <summary>
    /// Sets the status of an NPC being near the door and handles opening/closing the door.
    /// </summary>
    /// <param name="isNear">Is an NPC near the door.</param>
    public void SetNPCNear(bool isNear)
    {
        npcNear = isNear;

        if (npcNear && !opened)
        {
            OpenDoor();
        }
        else if (!npcNear && !playerNear)
        {
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    /// <summary>
    /// Coroutine to close the door after a delay if no one is near.
    /// </summary>
    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (!playerNear && !npcNear)
        {
            CloseDoor();
        }
    }

    /// <summary>
    /// Triggered when another collider enters the trigger collider.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerNear(true);
        }
        else if (other.CompareTag("NPC"))
        {
            SetNPCNear(true);
        }
    }

    /// <summary>
    /// Triggered when another collider exits the trigger collider.
    /// </summary>
    /// <param name="other">The collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetPlayerNear(false);
        }
        else if (other.CompareTag("NPC"))
        {
            SetNPCNear(false);
        }
    }
}

