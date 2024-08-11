using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingCutscene : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public LevelLoader levelLoader;


    private void OnEnable()
    {
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnDisable()
    {
        playableDirector.stopped -= OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            levelLoader.Restart(); // Load the main menu scene
        }
    }
}
