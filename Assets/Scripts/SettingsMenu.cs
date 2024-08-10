/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: This script is used for managing settings changed by player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Manages game settings such as volume, quality, and fullscreen mode.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    /// <summary>
    /// The audio mixer used to control game audio.
    /// </summary>
    public AudioMixer audioMixer;

    /// <summary>
    /// Sets the game volume.
    /// </summary>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log(volume) * 20);
    }

    /// <summary>
    /// Sets the quality level of the game.
    /// </summary>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Sets the game to fullscreen or windowed mode.
    /// </summary>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
