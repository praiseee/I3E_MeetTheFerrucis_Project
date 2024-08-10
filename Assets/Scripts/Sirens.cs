/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script controls the behavior of siren lights in the game. It alternates between 
 * a red light and a blue light with a specified delay to simulate a siren effect.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the siren lights in the game by alternating between a red light and a blue light.
/// The lights toggle on and off with a specified delay to create a siren effect.
/// </summary>
public class Sirens : MonoBehaviour
{
    /// <summary>
    /// The GameObject representing the red light.
    /// </summary>
    public GameObject redLight;

    /// <summary>
    /// The GameObject representing the blue light.
    /// </summary>
    public GameObject blueLight;

    /// <summary>
    /// The time to wait between toggling the lights, in seconds.
    /// </summary>
    public float waitTime = 0.2f;

    /// <summary>
    /// Initializes the siren effect by starting the coroutine.
    /// </summary>
    void Start()
    {
        StartCoroutine(Siren());
    }

    /// <summary>
    /// Coroutine that alternates the siren lights on and off with a delay.
    /// </summary>
    /// <returns>An IEnumerator for coroutine execution.</returns>
    IEnumerator Siren()
    {
        yield return new WaitForSeconds(waitTime);

        redLight.SetActive(false);
        blueLight.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        redLight.SetActive(true);
        blueLight.SetActive(false);
        StartCoroutine(Siren());
    }
}

