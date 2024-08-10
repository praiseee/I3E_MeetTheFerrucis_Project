/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script handles the main menu interactions, allowing the player to start the game or quit the application.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu interactions, including starting the game and quitting the application.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Starts the game by loading the specified scene.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(0); // Loads the scene with index 0 (typically the first level or main game scene).
    }

    /// <summary>
    /// Quits the game application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit(); // Exits the game application.
    }
}

