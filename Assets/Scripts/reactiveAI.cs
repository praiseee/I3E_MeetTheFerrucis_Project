using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class reactiveAI : MonoBehaviour
{
    public Transform[] waypoints;
    int waypointIndex;

    Vector3 target;
    NavMeshAgent agent;

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled && Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    public void StopMoving()
    {
        agent.isStopped = true; // Stop the agent from moving
    }

    public void ResumeMoving()
    {
        agent.isStopped = false; // Resume the agent's movement
        UpdateDestination(); // Update the destination to ensure it continues
    }
}

