using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Interactable currentInteractable;

    [SerializeField]
    Transform playerCamera;

    [SerializeField]
    float interactionDistance;

    [SerializeField]
    TextMeshProUGUI interactionText;

    Dialogue dialogue;

    public void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            if (hitInfo.transform.TryGetComponent<Interactable>(out currentInteractable))
            {
                interactionText.gameObject.SetActive(true);
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

    public void UpdateInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    void OnInteract()
    {
        if (dialogue == null || !dialogue.textActive)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact(this);
                dialogue = null;
            }
        }
    }

    void OnClick()
    {
        if (dialogue != null)
        {
            dialogue.SkipLine();
        }
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        dialogue = newDialogue;
    }
}

