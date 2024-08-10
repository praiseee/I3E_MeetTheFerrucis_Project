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
    public Dialogue sisterArgue;

    //Dialogue Brother
    public Dialogue brotherStarting;
    public Dialogue brotherIdle;
    public Dialogue brotherArgue;
    public Dialogue brotherKiller;
    public Dialogue brotherEnd;


    //Dialogue Father 
    public Dialogue fatherStarting;
    public Dialogue fatherIdle;
    public Dialogue fatherIdle2;
    public Dialogue fatherBank;
    public Dialogue fatherShoe;
    public Dialogue fatherFinal;
    public Dialogue fatherDone;

    //Dialogue Grandfather
    public Dialogue grandfatherIdle;
    public Dialogue grandfatherKiller;

    //Dialogue Player
    public Dialogue talkToFatherFirstSister;
    public Dialogue talkToFatherFirstBrother;

    //Dialogue chief
    public Dialogue chiefDialogue;

    //Conditions
    private static bool argueClueFound = false;
    private static bool sisterTwoDone = false;
    private static bool brotherOneDone = false;
    private static bool brotherDone = false;
    private static bool fatherOneDone = false;
    private static bool fatherTwoDone = false;
    private static bool fatherThreeDone = false;
    private static bool fatherFourDone = false;
    private static bool bankStatementsFound = false;
    private static bool shoeFound = false;
    private static bool motherLeave = false;
    private static bool killerOne = false;

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
        if(father)
        {
            if (argueClueFound && !bankStatementsFound && !motherLeave)
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
            else if (bankStatementsFound && !motherLeave)
            {
                if(!fatherThreeDone)
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
                if(!fatherFourDone)
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
        if(grandfather)
        {
            if (brotherDone)
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

    public void SisterTwoDone()
    {
        sisterTwoDone = true;
    }

    public void BrotherOneDone()
    {
        brotherOneDone = true;
    }

    public void BrotherDone()
    {
        brotherDone = true;
    }

    public void FatherOneDone()
    {
        fatherOneDone = true;
    }

    public void FatherTwoDone()
    {
        fatherTwoDone = true;
    }

    public void FatherThreeDone()
    {
        fatherThreeDone = true;
        brotherOneDone = true;
    }

    public void FatherFourDone()
    {
        fatherFourDone = true;
    }

    public void EvidenceTwoDone()
    {
        bankStatementsFound = true;
    }

    public void EvidenceThreeDone()
    {
        motherLeave = true;
    }

    public void EvidenceFourDone()
    {
        shoeFound = true;
    }

    public void KillerOne()
    {
        killerOne = true;
    }
}



