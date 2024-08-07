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

    public Dialogue outsideDoorDialogue;

    public Player player;

    /// <summary>
    /// Flags if the door is open
    /// </summary>
    bool opened = false;

    public bool sceneChanger = false;

    bool outsideDoorLocked = true;

    public int sceneToChange;

    /// <summary>
    /// Flags if the door is locked
    /// </summary>
    public bool locked = false;
    
    /// <summary>
    /// Flags if the player is near
    /// </summary>
    bool playerNear = false;

    public void UnlockOutsideDoor()
    {
        outsideDoorLocked = false;
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

        if (outsideDoorLocked)
        {
            outsideDoorDialogue.RunDialogue();
            player.SetDialogue(outsideDoorDialogue);
            return;
        }

        if(!locked && !opened)
        {
            // Create a new Vector3 and store the current rotation.
            Vector3 newRotation = transform.eulerAngles;

            // Add 90 degrees to the y axis rotation
            newRotation.y += 90f;

            // Assign the new rotation to the transform
            transform.eulerAngles = newRotation;

            // Set the opened bool to true
            opened = true;
        }

        if (locked)
        {

        }

        if (sceneChanger)
        {
            SceneManager.LoadScene(sceneToChange);
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
        if(!playerNear)
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

        // Close the door if the player is still not near.
        if(!playerNear)
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
    }
}


