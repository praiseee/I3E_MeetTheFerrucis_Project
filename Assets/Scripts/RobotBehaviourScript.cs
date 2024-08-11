/*
 * Author: Isaac Leong
 * Date: 11/08/2024
 * Description: 
 * This script controls a robot's movement in the game
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class RobotBehaviour : MonoBehaviour
{
    public Transform target; // The point the robot should move to
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public float normalSpeed = 3.5f; // Set this to the normal speed of your NavMeshAgent

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position); // Update the robot's destination
        }
    }

    /// <summary>
    /// Restores the robot's movement speed to its normal value.
    /// </summary>
    public void EnableMovement()
    {
        if (agent != null)
        {
            agent.speed = normalSpeed; // Restore the normal speed
        }
    }
}
