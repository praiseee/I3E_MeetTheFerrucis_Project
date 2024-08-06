using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;


	private bool evidenceOneDone = false;
	private bool evidenceTwoDone = false;
	private bool evidenceThreeDone = false;

	public TextMeshProUGUI scoreText;

	public void Awake()

	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(instance != null && instance != this)
		{
			Destroy(gameObject);
		}
	}
	public void EvidenceOneCheck()
    {
        evidenceOneDone = true;
		Debug.Log("OneDone");
    } 

	public void EvidenceTwoCheck()
    {
        evidenceTwoDone = true;
		Debug.Log("TwoDone");
    }

	public void EvidenceThreeCheck()
    {
        evidenceThreeDone = true;
		Debug.Log("ThreeDone");
    }
}
