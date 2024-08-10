using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class reactiveAI : MonoBehaviour
{
    public Transform[] waypoints;
    public Transform[] downstairsWaypoints;
    public Transform[] sittingDownWaypoints;
    public Door linkedDoor; // Reference to the Door script

    private bool useDownstairsWaypoints = false;
    private bool useSittingDownWaypoints = false;
    private bool destinationReached = false;

    int waypointIndex;
    Vector3 target;
    NavMeshAgent agent;

    void UpdateDestination()
    {
        if (useSittingDownWaypoints)
        {
            target = sittingDownWaypoints[waypointIndex].position;
        }
        else if (useDownstairsWaypoints)
        {
            target = downstairsWaypoints[waypointIndex].position;
        }
        else
        {
            target = waypoints[waypointIndex].position;
        }
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;

        // Check if the current waypoint index triggers the door
        if (waypointIndex == 3/* set the index that triggers the door */)
        {
            OpenAndCloseDoor();
        }

        if (useSittingDownWaypoints)
        {
            if (waypointIndex == 6)
            {
                agent.isStopped = true;
                destinationReached = true;
                return;
            }
        }
        else if (useDownstairsWaypoints)
        {
            if (waypointIndex == 8)
            {
                agent.isStopped = true;
                destinationReached = true;
                return;
            }
        }
        else
        {
            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }

    void OpenAndCloseDoor()
    {
        linkedDoor.OpenDoor();
        StartCoroutine(CloseDoorAfterDelay());
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        linkedDoor.CloseDoor();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if (agent.enabled && !agent.isStopped && Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void ResumeMoving()
    {
        if(!destinationReached)
        {
            agent.isStopped = false;
        }

        if(!agent.isStopped)
        {
            UpdateDestination();
        }
    }

    public void Downstairs()
    {
        StopMoving();
        waypointIndex = 0;
        useSittingDownWaypoints = false;
        useDownstairsWaypoints = true;
        ResumeMoving();
    }

    public void SittingDown()
    {
        StopMoving();
        waypointIndex = 0;
        useDownstairsWaypoints = false;
        useSittingDownWaypoints = true;
        ResumeMoving();
    }
}




