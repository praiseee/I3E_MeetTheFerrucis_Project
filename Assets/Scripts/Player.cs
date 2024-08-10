/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script manages the player's interactions with NPCs and evidence in the game. 
 * It handles detecting interactable objects, updating dialogue and evidence states, 
 * and managing the display of interaction text.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages player interactions with NPCs and evidence, including updating dialogue and evidence states,
/// and controlling the display of interaction and exit text.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// The current interactable object the player is aiming at.
    /// </summary>
    private Interactable currentInteractable;

    /// <summary>
    /// Reference to the player's camera transform.
    /// </summary>
    [SerializeField]
    private Transform playerCamera;

    /// <summary>
    /// The distance within which the player can interact with objects.
    /// </summary>
    [SerializeField]
    private float interactionDistance;

    /// <summary>
    /// The UI element displaying interaction text.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI interactionText;

    /// <summary>
    /// The UI element displaying exit text when the player is searching evidence.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI exitText;

    /// <summary>
    /// The current dialogue the player is engaged in.
    /// </summary>
    private Dialogue dialogue;

    /// <summary>
    /// The current evidence the player is interacting with.
    /// </summary>
    private Evidence evidence;

    /// <summary>
    /// Updates the interaction state each frame based on raycast detection of interactable objects.
    /// </summary>
    public void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            if (hitInfo.transform.TryGetComponent<Interactable>(out currentInteractable))
            {
                if (dialogue == null || !dialogue.textActive)
                {
                    if (evidence == null)
                    {
                        interactionText.gameObject.SetActive(true);
                        return;
                    }
                    else
                    {
                        if (evidence.searching)
                        {
                            return;
                        }
                    }
                    interactionText.gameObject.SetActive(true);
                }
                
            }
            else
            {
                currentInteractable = null;
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            currentInteractable = null;
            interactionText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Updates the current interactable object.
    /// </summary>
    /// <param name="newInteractable">The new interactable object.</param>
    public void UpdateInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    /// <summary>
    /// Resets the current dialogue.
    /// </summary>
    public void ResetDialogue()
    {
        dialogue = null;
    }

    /// <summary>
    /// Resets the current evidence.
    /// </summary>
    public void ResetEvidence()
    {
        evidence = null;
    }

    /// <summary>
    /// Toggles the inventory UI display.
    /// </summary>
    void OnInventory()
    {
        if (dialogue == null || !dialogue.textActive)
        {
            GameManager.instance.InventoryOn();
        }
    }

    /// <summary>
    /// Handles interaction with the current interactable object and hides the interaction text.
    /// </summary>
    void OnInteract()
    {
        interactionText.gameObject.SetActive(false);
        if (dialogue == null || !dialogue.textActive)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact(this);
            }
        }
    }

    /// <summary>
    /// Skips the current dialogue line if a dialogue is active.
    /// </summary>
    void OnClick()
    {
        if (dialogue != null)
        {
            dialogue.SkipLine();
        }
    }

    /// <summary>
    /// Sets the current dialogue.
    /// </summary>
    /// <param name="newDialogue">The new dialogue to set.</param>
    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
        Debug.Log(dialogue);
    }

    /// <summary>
    /// Sets the current evidence.
    /// </summary>
    /// <param name="newEvidence">The new evidence to set.</param>
    public void SetEvidence(Evidence newEvidence)
    {
        evidence = newEvidence;
        Debug.Log(dialogue);
    }

    /// <summary>
    /// Updates the display of interaction and exit text based on evidence searching state.
    /// </summary>
    public void ExitText()
    {
        if (evidence.searching)
        {
            interactionText.gameObject.SetActive(false);
            exitText.gameObject.SetActive(true);
        }
        else
        {
            interactionText.gameObject.SetActive(true);
            exitText.gameObject.SetActive(false);
        }
    }
}


