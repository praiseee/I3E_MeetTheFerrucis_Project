/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script manages animation for siren.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sirens : MonoBehaviour
{
    public GameObject redLight;
    public GameObject blueLight;
    public float waitTime = .2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Siren());
    }

    /// <summary>
    /// Coroutine to alternate the activation of red and blue lights to simulate a siren effect.
    /// </summary>
    IEnumerator Siren()
    {
        yield return new WaitForSeconds(waitTime);

        redLight.SetActive(false);
        blueLight.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        redLight.SetActive(true); // Turn on the red light
        blueLight.SetActive(false);
        StartCoroutine(Siren()); // Restart the coroutine for continuous effect
    }

}
