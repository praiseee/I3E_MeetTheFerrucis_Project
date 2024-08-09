using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    public bool textActive = false;
    public bool evidence = false;

    private int index;

    [SerializeField]
    private GameObject buttonsHide;

    [SerializeField]
    private GameObject convoButtons;

    public Player player;

    public NPC npc;

    // Struct to hold the dialogue line text and associated GameObject for UI
    [System.Serializable]
    public struct DialogueLine
    {
        public string lineText;
        public GameObject speakerUI; // Associated GameObject for this line (e.g., a panel with speaker's portrait)
    }

    public DialogueLine[] dialogueLines; // Array of DialogueLine structs

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        DeactivateAllSpeakerUI();
    }

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

    IEnumerator DisplayFirstLineWithDelay()
    {
        yield return new WaitForSeconds(0.01f);  // Slight delay before showing the first line to ensure proper initialization
        DisplayLine();
    }

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

    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (char c in dialogueLines[index].lineText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

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
                npc.ExitDialogue();
                gameObject.SetActive(false);
                DialogueEnd();
                buttonsHide.SetActive(true);
            }
        }
    }

    public void DialogueEnd()
    {
       
        textActive = false;
        DeactivateAllSpeakerUI();
    }

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

    // Deactivate all speaker UI elements
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






