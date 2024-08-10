using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : Interactable
{
    [SerializeField]
    private AudioClip collectAudio;


    bool opened = false;
    bool opening = false;

    static bool disabled = true;

    public float moveDistance = -2f; // Distance to move forward
    public float backDistance = 1f;
    public float moveDuration = 1f; // Duration of the movement

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    public void Collected()
    {
        // Start the coroutine to move the object forward
        StartCoroutine(MoveForward());
        
    }

    public override void Interact(Player thePlayer)
    {
        if(!disabled)
        {
            if (!opening)
            {
                opening = true;
                base.Interact(thePlayer);
                AudioSource.PlayClipAtPoint(collectAudio, transform.position, 1f);
                Collected();
            }
        }
        

        
    }

    private IEnumerator MoveForward()
    {
        initialPosition = transform.position;
        if (!opened)
        {
            targetPosition = transform.position + transform.forward * moveDistance;
            opened = true;
        }
        else
        {
            targetPosition = transform.position + transform.forward * backDistance;
            opened = false;
        }
        

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        opening = false;
    }

    public void Unlock()
    {
        disabled = false;
    }
}

