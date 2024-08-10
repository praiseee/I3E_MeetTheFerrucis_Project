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

    
    public TextMeshProUGUI evidenceObjective;

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

    [SerializeField]
    private GameObject evidenceInventory4;

    [SerializeField]
    private GameObject evidenceInventory5;

    [SerializeField]
    private GameObject evidenceInventory6;

    [SerializeField]
    private GameObject evidenceInventory7;

    [SerializeField]
    private GameObject evidenceInventory8;
    
    [SerializeField]
    private GameObject evidenceInventory9;

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

    public void ArguementInventory()
    {
        evidenceInventory4.gameObject.SetActive(true);
        //evidenceObjective.text = "Talk to the Father";
    }

    public void BankStatementInventory()
    {
        evidenceInventory5.gameObject.SetActive(true);
        //evidenceObjective.text = "Talk to the Father";
    }

    public void VictimLeavingInventory()
    {
        evidenceInventory6.gameObject.SetActive(true);
        //evidenceObjective.text = "Talk to the Father";
    }

    public void ShoeInventory()
    {
        evidenceInventory7.gameObject.SetActive(true);
        //evidenceObjective.text = "Talk to the Father";
    }

    public void DadProtectInventory()
    {
        evidenceInventory8.gameObject.SetActive(true);
        //evidenceObjective.text = "Talk to the Son";
    }

    public void KeyInventory()
    {
        evidenceInventory9.gameObject.SetActive(true);
        //evidenceObjective.text = "Talk to the Grandfather";
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            inventory.gameObject.SetActive(true);
            inventoryShown = true;
            return;
        }

        if(inventoryShown)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inventory.gameObject.SetActive(false);
            inventoryShown = false;
        }
        
    }
}

