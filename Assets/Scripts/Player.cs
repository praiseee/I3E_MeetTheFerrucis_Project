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

    [SerializeField]
    TextMeshProUGUI exitText;

    private Dialogue dialogue;

    public Evidence evidence;

    public void Update()
    {
        //Debug.Log(dialogue);
        RaycastHit hitInfo;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo, interactionDistance))
        {
            if (hitInfo.transform.TryGetComponent<Interactable>(out currentInteractable))
            {
                if(dialogue == null || !dialogue.textActive )
                {
                    if (evidence.searching)
                    {
                        return;
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

    public void UpdateInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    public void ResetDialogue()
    {
        dialogue = null;
    }

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

    void OnClick()
    {
        //Debug.Log(dialogue);

        if (dialogue != null)
        {
            dialogue.SkipLine();
        }
    }

    public void SetDialogue(Dialogue newDialogue)
    {
        //Debug.Log("set dialog");
        //Debug.Log(newDialogue);
        dialogue = newDialogue;
        Debug.Log(dialogue);
        return;
    }

    public void ExitText()
    {
        if(evidence.searching)
        {
            interactionText.gameObject.SetActive(false);
            exitText.gameObject.SetActive(true);

        }
        if(!evidence.searching)
        {
            interactionText.gameObject.SetActive(true);
            exitText.gameObject.SetActive(false);
        }
    }
}

