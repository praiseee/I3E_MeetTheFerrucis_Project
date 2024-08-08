using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int evidenceDone = 0;
    private bool inventoryShown = false;

    [SerializeField]
    TextMeshProUGUI outsidePoliceObjective;

    [SerializeField]
    TextMeshProUGUI evidenceObjective;

    [SerializeField]
    private GameObject gateBorder;

    [SerializeField]
    private GameObject inventory;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void EvidenceOneCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        Debug.Log("OneDone");
    }

    public void EvidenceTwoCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        Debug.Log("TwoDone");
    }

    public void EvidenceThreeCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        Debug.Log("ThreeDone");
    }

    public void OutsidePoliceDone()
    {
        gateBorder.SetActive(false);
        outsidePoliceObjective.gameObject.SetActive(false);
        evidenceObjective.gameObject.SetActive(true);
    }

    public void InventoryOn()
    {
        if(!inventoryShown)
        {
            inventory.gameObject.SetActive(true);
        }

        if(inventoryShown)
        {
            inventory.gameObject.SetActive(false);
        }
        
    }
}

