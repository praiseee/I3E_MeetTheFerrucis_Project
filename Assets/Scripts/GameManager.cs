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

    //Evidence Inventory
    [SerializeField]
    private GameObject evidenceInventory1;

    [SerializeField]
    private GameObject evidenceInventory2;

    [SerializeField]
    private GameObject evidenceInventory3;

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
        evidenceInventory1.gameObject.SetActive(true);
        Debug.Log("OneDone");
    }

    public void EvidenceTwoCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        evidenceInventory2.gameObject.SetActive(true);
        Debug.Log("TwoDone");
    }

    public void EvidenceThreeCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        evidenceInventory3.gameObject.SetActive(true);
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
            inventoryShown = true;
            return;
        }

        if(inventoryShown)
        {
            inventory.gameObject.SetActive(false);
            inventoryShown = false;
        }
        
    }
}

