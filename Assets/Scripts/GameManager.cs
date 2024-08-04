using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	private int currentScore = 0;

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
	public void IncreaseScore(int scoreToAdd)
    {
        // Increase the score of the player by scoreToAdd
        currentScore += scoreToAdd;
		scoreText.text = "Score: " + currentScore.ToString();
    } 

}
