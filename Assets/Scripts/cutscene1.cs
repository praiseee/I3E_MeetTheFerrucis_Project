using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class cutscene1 : MonoBehaviour
{
    public Transform target; // The point the robot should move to
    private NavMeshAgent agent;
    public float normalSpeed = 3.5f; // Set this to the normal speed of your NavMeshAgent
    public PlayableDirector playableDirector; // Reference to the PlayableDirector for cutscenes
    public LevelLoader levelLoader;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        else
        {
            agent.speed = 0; // Set the speed to zero initially
        }

        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnDestroy()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    void Update()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    public void EnableMovement()
    {
        if (agent != null)
        {
            agent.speed = normalSpeed; // Restore the normal speed
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            levelLoader.SwitchScene();
            //SceneManager.LoadScene("Outside"); // Transition to the "Outside" scene
        }
    }
}
