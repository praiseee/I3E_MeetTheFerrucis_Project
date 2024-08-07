using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    public bool textActive = false;
    public bool evidence = false;
    private int index;

    [SerializeField]
    private GameObject buttonsHide;

    [SerializeField]
    private GameObject convoButtons;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (textActive)
        {   
           // buttonsHide.gameObject.SetActive(false);
        }

        if (!textActive)
        {
           // buttonsHide.gameObject.SetActive(true);
        }
    }

    public void RunDialogue()
    {
        if(evidence)
        {
            buttonsHide.gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        StartDialogue();
        textActive = true;
        
    }

    void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = 0;
        if(!evidence)
        {
            convoButtons.gameObject.SetActive(false);
        }
        
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if(!evidence)
            {
                convoButtons.gameObject.SetActive(true);
            }

            
            
            if(evidence)
            {
                player.ResetDialogue();
                gameObject.SetActive(false);
                DialogueEnd();
                buttonsHide.gameObject.SetActive(true);
            }
            

        }
    }

    public void DialogueEnd()
    {
        textActive = false;
    }

    public void SkipLine()
    {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        
    }
}
