/*
 * Author:Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * Manages the dialogue for the game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Dialogue class handles displaying a sequence of dialogue lines with associated speaker UI.
/// It manages the progression of dialogue, typing effect, and visibility of UI elements based on the state of the dialogue.
/// </summary>
public class Dialogue : MonoBehaviour
{
    /// <summary>
    /// Reference to the TextMeshProUGUI component that displays the dialogue text.
    /// </summary>
    public TextMeshProUGUI textComponent;

    /// <summary>
    /// Speed at which each character of the dialogue text appears.
    /// </summary>
    public float textSpeed;

    /// <summary>
    /// Flag indicating whether the dialogue is currently active.
    /// </summary>
    public bool textActive = false;

    /// <summary>
    /// Flag indicating whether the dialogue is part of an evidence sequence.
    /// </summary>
    public bool evidence = false;

    /// <summary>
    /// Index of the current dialogue line being displayed.
    /// </summary>
    private int index;

    /// <summary>
    /// Reference to the GameObject that contains buttons to be hidden during dialogue.
    /// </summary>
    [SerializeField]
    private GameObject buttonsHide;

    /// <summary>
    /// Reference to the GameObject that contains buttons for conversation actions.
    /// </summary>
    [SerializeField]
    private GameObject convoButtons;

    /// <summary>
    /// Reference to the Player object.
    /// </summary>
    public Player player;

    /// <summary>
    /// Reference to the NPC object.
    /// </summary>
    public NPC npc;

    /// <summary>
    /// Struct representing a line of dialogue and its associated speaker UI element.
    /// </summary>
    [System.Serializable]
    public struct DialogueLine
    {
        /// <summary>
        /// The text of the dialogue line.
        /// </summary>
        public string lineText;

        /// <summary>
        /// The GameObject associated with the speaker of this dialogue line (e.g., a panel with the speaker's portrait).
        /// </summary>
        public GameObject speakerUI;
    }

    /// <summary>
    /// Array of DialogueLine structs, representing the entire dialogue sequence.
    /// </summary>
    public DialogueLine[] dialogueLines;

    /// <summary>
    /// Initializes the dialogue by clearing the text and deactivating all speaker UI elements.
    /// </summary>
    void Start()
    {
        textComponent.text = string.Empty;
        DeactivateAllSpeakerUI();
    }

    /// <summary>
    /// Begins the dialogue sequence.
    /// </summary>
    public void RunDialogue()
    {
        textActive = true;
        index = 0;
        gameObject.SetActive(true);

        if (evidence)
        {
            buttonsHide.SetActive(false);
        }
        if (!evidence)
        {
            convoButtons.SetActive(false);
        }

        StartCoroutine(DisplayFirstLineWithDelay());
    }

    /// <summary>
    /// Displays the first line of dialogue after a short delay.
    /// </summary>
    IEnumerator DisplayFirstLineWithDelay()
    {
        yield return new WaitForSeconds(0.01f);  // Slight delay before showing the first line to ensure proper initialization
        DisplayLine();
    }

    /// <summary>
    /// Displays the current line of dialogue and its associated speaker UI element.
    /// </summary>
    void DisplayLine()
    {
        DeactivateAllSpeakerUI();

        if (dialogueLines[index].speakerUI != null)
        {
            dialogueLines[index].speakerUI.SetActive(true);
        }

        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// Types out the current line of dialogue character by character.
    /// </summary>
    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (char c in dialogueLines[index].lineText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// Moves to the next line of dialogue or ends the dialogue sequence if at the end.
    /// </summary>
    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            DisplayLine();
        }
        else
        {
            if (!evidence)
            {
                convoButtons.SetActive(true);
            }

            if (evidence)
            {
                player.ResetDialogue();
                if(npc != null)
                {
                    npc.ExitDialogue();
                }
                gameObject.SetActive(false);
                DialogueEnd();
                buttonsHide.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Ends the dialogue sequence, deactivating the dialogue UI and resetting necessary states.
    /// </summary>
    public void DialogueEnd()
    {
        textActive = false;
        DeactivateAllSpeakerUI();
    }

    /// <summary>
    /// Skips to the end of the current line or advances to the next line if the current line is fully displayed.
    /// </summary>
    public void SkipLine()
    {
        if (textComponent.text == dialogueLines[index].lineText)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = dialogueLines[index].lineText;
        }
    }

    /// <summary>
    /// Deactivates all speaker UI elements associated with the dialogue.
    /// </summary>
    private void DeactivateAllSpeakerUI()
    {
        foreach (var line in dialogueLines)
        {
            if (line.speakerUI != null)
            {
                line.speakerUI.SetActive(false);
            }
        }
    }
}







