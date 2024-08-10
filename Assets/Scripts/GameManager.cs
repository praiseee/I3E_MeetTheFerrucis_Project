/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script manages the game's overall state, including tracking evidence collection, updating objectives, and handling inventory display.
 * It also controls certain game events, such as unlocking areas and changing objectives based on player progress.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the overall game state, including evidence tracking, objective updates, and inventory management.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the GameManager.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Tracks the number of evidence pieces collected.
    /// </summary>
    int evidenceDone = 0;

    /// <summary>
    /// Flag indicating whether the inventory is currently shown.
    /// </summary>
    private bool inventoryShown = false;

    /// <summary>
    /// Reference to the UI text that shows the outside police objective.
    /// </summary>
    [SerializeField]
    TextMeshProUGUI outsidePoliceObjective;

    /// <summary>
    /// Reference to the UI text that shows the current evidence objective.
    /// </summary>
    public TextMeshProUGUI evidenceObjective;

    /// <summary>
    /// Reference to the gate border GameObject that is activated/deactivated based on player progress.
    /// </summary>
    [SerializeField]
    private GameObject gateBorder;

    /// <summary>
    /// Reference to the inventory UI GameObject.
    /// </summary>
    [SerializeField]
    private GameObject inventory;

    // References to the evidence inventory UI elements.
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

    /// <summary>
    /// Initializes the GameManager singleton instance.
    /// </summary>
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

    /// <summary>
    /// Updates the game state when the first piece of evidence is found.
    /// </summary>
    public void EvidenceOneCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        evidenceInventory1.gameObject.SetActive(true);
        Debug.Log("OneDone");
    }

    /// <summary>
    /// Updates the game state when the second piece of evidence is found.
    /// </summary>
    public void EvidenceTwoCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        evidenceInventory2.gameObject.SetActive(true);
        Debug.Log("TwoDone");
    }

    /// <summary>
    /// Updates the game state when the third piece of evidence is found.
    /// </summary>
    public void EvidenceThreeCheck()
    {
        evidenceDone += 1;
        evidenceObjective.text = "Investigate the Scene " + evidenceDone.ToString() + "/3";
        evidenceInventory3.gameObject.SetActive(true);
        Debug.Log("ThreeDone");
    }

    /// <summary>
    /// Activates the inventory UI element for the argument evidence.
    /// </summary>
    public void ArguementInventory()
    {
        evidenceInventory4.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates the inventory UI element for the bank statement evidence.
    /// </summary>
    public void BankStatementInventory()
    {
        evidenceInventory5.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates the inventory UI element for the victim leaving evidence.
    /// </summary>
    public void VictimLeavingInventory()
    {
        evidenceInventory6.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates the inventory UI element for the shoe evidence.
    /// </summary>
    public void ShoeInventory()
    {
        evidenceInventory7.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates the inventory UI element for the dad's protection evidence.
    /// </summary>
    public void DadProtectInventory()
    {
        evidenceInventory8.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates the inventory UI element for the key evidence.
    /// </summary>
    public void KeyInventory()
    {
        evidenceInventory9.gameObject.SetActive(true);
    }

    /// <summary>
    /// Handles the event when the outside police objective is completed, unlocking the gate and updating objectives.
    /// </summary>
    public void OutsidePoliceDone()
    {
        gateBorder.SetActive(false);
        outsidePoliceObjective.gameObject.SetActive(false);
        evidenceObjective.gameObject.SetActive(true);
    }

    /// <summary>
    /// Toggles the inventory UI on and off, managing cursor visibility and lock state.
    /// </summary>
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


