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

    private Evidence evidence;

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
                    if (evidence == null)
                    {
                        interactionText.gameObject.SetActive(true);
                        return;
                    }
                    else
                    {
                        if(evidence.searching)
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

    public void UpdateInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
    }

    public void ResetDialogue()
    {
        dialogue = null;
    }

    public void ResetEvidence()
    {
        evidence = null;
    }

    void OnInventory()
    {
        GameManager.instance.InventoryOn();
        Debug.Log("q");
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

    public void SetEvidence(Evidence newEvidence)
    {
        evidence = newEvidence;
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

