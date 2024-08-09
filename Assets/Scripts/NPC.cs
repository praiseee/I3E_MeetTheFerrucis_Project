using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    bool talking = false;
    public Player player;
    [SerializeField]
    private MonoBehaviour playerMovementScript;
    private Quaternion originalRotation;
    [SerializeField]
    private float rotationSpeed = 2.0f;

    // Reference to the reactiveAI script
    public reactiveAI npcMovement;

    //NPCs
    public bool sister = false;
    public bool brother = false;
    public bool father = false;
    public bool grandfather = false;
    public bool chief = false;


    //Dialogue Sister
    public Dialogue sisterStarting;
    public Dialogue sisterIdle;

    //Dialogue Brother
    public Dialogue brotherStarting;
    public Dialogue brotherIdle;

    //Dialogue Father 
    public Dialogue fatherStarting;
    public Dialogue fatherIdle;
    public Dialogue fatherIdle2;

    //Dialogue Player
    public Dialogue talkToFatherFirstSister;
    public Dialogue talkToFatherFirstBrother;

    //Dialogue chief
    public Dialogue chiefDialogue;

    //Conditions
    private static bool argueClueFound = false;
    private static bool brotherOneDone = false;
    private static bool fatherOneDone = false;
    private static bool fatherTwoDone = false;
    private static bool bankStatementsFound = false;


    private void Awake()
    {
        //npcMovement = GetComponent<reactiveAI>();
    }

    public override void Interact(Player thePlayer)
    {
        if (!talking)
        {
            talking = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Stop the NPC's movement
            if(father)
            {
                npcMovement.StopMoving();
            }
            

            // Store the original rotation only if it's the first time talking
            if (!IsInvoking("StoreOriginalRotation"))
            {
                StoreOriginalRotation();
            }

            // Start the coroutine to look at the player, using the current rotation as the starting point
            StopAllCoroutines(); // Stop any existing rotation coroutines to avoid conflicts
            StartCoroutine(SmoothLookAt(player.transform));

            StartDialogue();
        
            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            base.Interact(thePlayer);
        }
    }

    public void StartDialogue()
    {
        if(sister)
        {
            if(!argueClueFound)
            {
                if(!fatherOneDone)
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
            else
            {
                sisterIdle.RunDialogue();
                player.SetDialogue(sisterIdle);
            }
        }
        if(brother)
        {
            if (!brotherOneDone)
            {
                if(!fatherOneDone)
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
            else
            {
                brotherIdle.RunDialogue();
                player.SetDialogue(brotherIdle);
            }
            
        }
        if(father)
        {
            if (argueClueFound)
            {
                if(!fatherTwoDone)
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
            else if (bankStatementsFound)
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
        if(grandfather)
        {

        }
        if(chief)
        {
            chiefDialogue.RunDialogue();
            player.SetDialogue(chiefDialogue);
        }

        //dialogue.RunDialogue();
        //player.SetDialogue(dialogue);
    }

    private void StoreOriginalRotation()
    {
        originalRotation = transform.rotation;
    }

    public void ExitDialogue()
    {
        talking = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Start the coroutine to return to the original rotation
        StopAllCoroutines(); // Stop any existing rotation coroutines to avoid conflicts
        StartCoroutine(SmoothReturnToOriginalRotation());

        // Resume the NPC's movement
        if(father)
        {
            npcMovement.ResumeMoving();
        }


        // Enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }

    private IEnumerator SmoothLookAt(Transform target)
    {
        if(brother)
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

    public void EvidenceOneDone()
    {
        argueClueFound = true;
    }

    public void BrotherOneDone()
    {
        brotherOneDone = true;
    }

    public void FatherOneDone()
    {
        fatherOneDone = true;
    }

    public void FatherTwoDone()
    {
        fatherTwoDone = true;
    }

    public void EvidenceTwoDone()
    {
        bankStatementsFound = true;
    }
}



