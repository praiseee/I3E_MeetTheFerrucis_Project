/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script handles the scene transitions in the game, including triggering animations and loading the next scene.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene transitions, including triggering animations and loading the next scene.
/// </summary>
public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// Reference to the Animator component used for the transition animation.
    /// </summary>
    public Animator transition;

    /// <summary>
    /// Initiates the process to switch to the next scene.
    /// </summary>
    public void SwitchScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    /// <summary>
    /// Coroutine to handle the scene loading process, including playing a transition animation.
    /// </summary>
    /// <param name="levelIndex">The index of the scene to load.</param>
    /// <returns>IEnumerator for coroutine management.</returns>
    IEnumerator LoadLevel(int levelIndex)
    {
        // Trigger the start of the transition animation.
        transition.SetTrigger("Start");

        // Wait for the duration of the transition animation.
        yield return new WaitForSeconds(1f);

        // Load the specified scene.
        SceneManager.LoadScene(levelIndex);

        // Lock the cursor and hide it for the next scene.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



}

