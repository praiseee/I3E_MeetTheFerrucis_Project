using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StopMovementAndAnimation : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public PlayableDirector playableDirector; // Reference to the PlayableDirector for cutscenes

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

    void OnDestroy()
    {
        // Remove event listener when the script is destroyed
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

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

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            SceneManager.LoadScene("Outside"); // Transition to the "Outside" scene
        }
    }
}
