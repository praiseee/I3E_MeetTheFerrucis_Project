/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script manages the interaction with a shelf object in the game, allowing it to open and close 
 * with animation. It also handles the audio playback when the shelf is interacted with.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages interactions with a shelf object in the game. Handles opening and closing animations,
/// plays audio when interacted with, and includes functionality to unlock the shelf for interaction.
/// </summary>
public class Shelf : Interactable
{
    /// <summary>
    /// Audio clip to play when the shelf is interacted with.
    /// </summary>
    [SerializeField]
    private AudioClip openAudio;

    /// <summary>
    /// Flag to indicate if the shelf is currently opened.
    /// </summary>
    private bool opened = false;

    /// <summary>
    /// Flag to indicate if the shelf is currently in the process of opening or closing.
    /// </summary>
    private bool opening = false;

    /// <summary>
    /// Flag to indicate if the shelf interaction is disabled.
    /// </summary>
    static private bool disabled = true;

    /// <summary>
    /// Distance to move the shelf forward when opening.
    /// </summary>
    public float moveDistance = -2f;

    /// <summary>
    /// Distance to move the shelf backward when closing.
    /// </summary>
    public float backDistance = 1f;

    /// <summary>
    /// Duration of the opening/closing movement.
    /// </summary>
    public float moveDuration = 1f;

    /// <summary>
    /// Initial position of the shelf.
    /// </summary>
    private Vector3 initialPosition;

    /// <summary>
    /// Target position of the shelf after movement.
    /// </summary>
    private Vector3 targetPosition;

    /// <summary>
    /// Starts the coroutine to move the shelf forward or backward.
    /// </summary>
    public void Collected()
    {
        StartCoroutine(MoveForward());
    }

    /// <summary>
    /// Handles interaction with the shelf. Plays audio and starts the movement coroutine if the shelf is not disabled.
    /// </summary>
    /// <param name="thePlayer">The player interacting with the shelf.</param>
    public override void Interact(Player thePlayer)
    {
        if (!disabled)
        {
            if (!opening)
            {
                opening = true;
                base.Interact(thePlayer);
                AudioSource.PlayClipAtPoint(openAudio, transform.position, 1f);
                Collected();
            }
        }
    }

    /// <summary>
    /// Moves the shelf forward or backward based on its current state. Animates the movement using linear interpolation.
    /// </summary>
    /// <returns>An IEnumerator for coroutine execution.</returns>
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

    /// <summary>
    /// Unlocks the shelf for interaction by setting the disabled flag to false.
    /// </summary>
    public void Unlock()
    {
        disabled = false;
    }
}


