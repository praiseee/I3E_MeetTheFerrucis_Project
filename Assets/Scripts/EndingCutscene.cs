/*
 * Author: Isaac Leung
 * Date: 11/08/2024
 * Description: 
 * This script manages the ending cutscene in the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingCutscene : MonoBehaviour
{
    public PlayableDirector playableDirector; // Reference to the PlayableDirector for the ending cutscene
    public LevelLoader levelLoader; // Reference to the LevelLoader for scene transitions

    private void OnEnable()
    {
        // Subscribe to the PlayableDirector's stopped event
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnDisable()
    {
        // Unsubscribe from the PlayableDirector's stopped event
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }

    /// <summary>
    /// Handles the event when the PlayableDirector stops. 
    /// Triggers a scene transition to restart the game or load the main menu.
    /// </summary>
    /// <param name="director">The PlayableDirector that stopped.</param>
    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            levelLoader.Restart(); // Load the main menu scene
        }
    }
}
