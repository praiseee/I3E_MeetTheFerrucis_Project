using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    bool talking = false;
    public Dialogue dialogue;
    public Player player;
    [SerializeField]
    private MonoBehaviour playerMovementScript;
    private Quaternion originalRotation;
    [SerializeField]
    private float rotationSpeed = 2.0f;

    public override void Interact(Player thePlayer)
    {
        if (!talking)
        {
            talking = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Store the original rotation
            originalRotation = transform.rotation;

            // Start the coroutine to look at the player
            StartCoroutine(SmoothLookAt(player.transform));

            dialogue.RunDialogue();
            player.SetDialogue(dialogue);

            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }

            base.Interact(thePlayer);
        }
    }

    public void ExitDialogue()
    {
        talking = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

        // Start the coroutine to return to the original rotation
        StartCoroutine(SmoothLookAt(originalRotation));
    }

    private IEnumerator SmoothLookAt(Transform target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        yield return SmoothLookAt(targetRotation);
    }

    private IEnumerator SmoothLookAt(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}


