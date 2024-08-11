/*
 * Author: Isaac Leong
 * Date: 11/08/2024
 * Description: 
 * This script stops a character's movement and animation during a cutscene and 
 * triggers a scene transition when the cutscene ends. It utilizes a NavMeshAgent 
 * for movement and an Animator for character animations, and listens to the 
 * PlayableDirector to determine when the cutscene is complete.
 */

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles stopping the movement and animation of a character during a cutscene
/// </summary>
public class StopMovementAndAnimation : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public PlayableDirector playableDirector; // Reference to the PlayableDirector for cutscenes
    public LevelLoader levelLoader;

    /// <summary>
    /// Initializes references to NavMeshAgent and Animator components.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        // Add event listener for when the PlayableDirector stops
        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    /// <summary>
    /// Removes the event listener when the script is destroyed.
    /// </summary>
    void OnDestroy()
    {
        // Remove event listener when the script is destroyed
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    /// <summary>
    /// Stops the movement by setting the NavMeshAgent speed to zero
    /// </summary>
    public void Stop()
    {
        if (agent != null)
        {
            agent.speed = 0; // Stop the movement by setting the speed to zero
        }

        if (animator != null)
        {
            animator.enabled = false; // Stop the animation
        }
    }

    /// <summary>
    /// Handles the event when the PlayableDirector stops, triggering a scene transition.
    /// </summary>
    /// <param name="director"></param>
    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            levelLoader.SwitchScene();
            //SceneManager.LoadScene("Outside"); // Transition to the "Outside" scene
        }
    }
}
