/*
 * Author: Elyas Chua-Aziz
 * Date: 06/05/2024
 * Description: 
 * The door that opens when the player is near it and presses the interact button.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : Interactable
{
    [SerializeField]
    TextMeshProUGUI lockedDoorText;

    [SerializeField]
    private AudioSource interactionSound;

    [SerializeField]
    private AudioSource lockedSound;

    public Dialogue outsideDoorDialogue;

    public Dialogue insideDoorDialogue;

    public Player player;

    /// <summary>
    /// Flags if the door is open
    /// </summary>
    bool opened = false;

    public bool outsideDoor = false;

    public bool insideDoor = false;

    public bool sceneChanger = false;

    public LevelLoader levelLoader;

    bool outsideDoorLocked = true;

    bool insideDoorLocked = true;


    /// <summary>
    /// Flags if the door is locked
    /// </summary>
    public bool locked = false;
    
    /// <summary>
    /// Flags if the player is near
    /// </summary>
    bool playerNear = false;

    /// <summary>
    /// Flags if an NPC is near
    /// </summary>
    bool npcNear = false;

    public void UnlockOutsideDoor()
    {
        outsideDoorLocked = false;
    }

    public void UnlockInsideDoor()
    {
        insideDoorLocked = false;
    }

    /// <summary>
    /// Handles the door's interaction
    /// </summary>
    /// <param name="thePlayer">The player that interacted with the door</param>
    public override void Interact(Player thePlayer)
    {
        // Call the Interact function from the base Interactable class.
        base.Interact(thePlayer);

        // Call the OpenDoor() function
        OpenDoor();
    }

    /// <summary>
    /// Handles the door opening 
    /// </summary>
    public void OpenDoor()
    {
        // Door should open only when it is not locked
        // and not already opened.
        if (outsideDoor)
        {
            if (outsideDoorLocked)
            {
                outsideDoorDialogue.RunDialogue();
                player.SetDialogue(outsideDoorDialogue);
                return;
            }
        }

        if (insideDoor)
        {
            if (insideDoorLocked)
            {
                insideDoorDialogue.RunDialogue();
                player.SetDialogue(insideDoorDialogue);
                return;
            }
        }
        if(locked && !insideDoor && !outsideDoor)
        {
           lockedSound.Play();
        }

        if(!locked && !opened)
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

            if(outsideDoor)
            {
                GameManager.instance.evidenceObjective.text = "Talk to the Dad";
            }
            if(insideDoor)
            {
                GameManager.instance.evidenceObjective.gameObject.SetActive(false);
            }

        }
    }

    /// <summary>
    /// Handles the door closing 
    /// </summary>
    public void CloseDoor()
    {
        // Door should close only when it is not locked
        // and already opened.
        if(!locked && opened)
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
    /// <param name="lockStatus">The lock status of the door</param>
    public void SetLock(bool lockStatus)
    {
        // Assign the lockStatus value to the locked bool.
        locked = lockStatus;
    }

    /// <summary>
    /// Sets the player near status and starts the coroutine to close the door.
    /// </summary>
    /// <param name="isNear">Is the player near the door</param>
    public void SetPlayerNear(bool isNear)
    {
        // Assign the isNear value to the playerNear bool.
        playerNear = isNear;

        // If the player is no longer near, start the coroutine to close the door.
        if(!playerNear && !npcNear)
        {
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    /// <summary>
    /// Sets the NPC near status and handles door opening.
    /// </summary>
    /// <param name="isNear">Is an NPC near the door</param>
    public void SetNPCNear(bool isNear)
    {
        // Assign the isNear value to the npcNear bool.
        npcNear = isNear;

        // If the NPC is near and the door isn't already opened, open the door.
        if (npcNear && !opened)
        {
            OpenDoor();
        }
        // If the NPC is no longer near and the player is also not near, start the coroutine to close the door.
        else if (!npcNear && !playerNear)
        {
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    /// <summary>
    /// Coroutine to close the door after a delay.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CloseDoorAfterDelay()
    {
        // Wait for 3 seconds.
        yield return new WaitForSeconds(1f);

        // Close the door if neither the player nor an NPC is near.
        if(!playerNear && !npcNear)
        {
            CloseDoor();
        }
    }

    /// <summary>
    /// Called when another collider enters the trigger collider.
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SetPlayerNear(true);
        }
        else if (other.CompareTag("NPC"))
        {
            SetNPCNear(true);
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger collider.
    /// </summary>
    /// <param name="other">The collider that exited the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SetPlayerNear(false);
        }
        else if (other.CompareTag("NPC"))
        {
            SetNPCNear(false);
        }
    }
}



