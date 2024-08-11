/*
 * Author: Hoo Ying Qi Praise
 * Date: 11/08/2024
 * Description: 
 * The AI using sets of waypoints, handles behaviors for sitting down, going downstairs, and interacting with doors.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class reactiveAI : MonoBehaviour
{
    /// <summary>
    /// WayPoints for patrolling
    /// </summary>
    public Transform[] waypoints;
    public Transform[] downstairsWaypoints;
    public Transform[] sittingDownWaypoints;

    /// <summary>
    /// Reference to the Door script for handling door interactions
    /// </summary>
    public Door linkedDoor; 
    public Door linkedDoor2;
    public Door linkedDoor3;

    private bool useDownstairsWaypoints = false;
    private bool useSittingDownWaypoints = false;
    public bool destinationReached = false;

    public bool police = false;

    public Animator npcAnimator;

    int waypointIndex;
    Vector3 target;
    NavMeshAgent agent;

    /// <summary>
    /// Updates the AI's destination to the current waypoint
    /// </summary>
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

    /// <summary>
    /// Increments the waypoint index and checks if a door should be opened/closed
    /// </summary>
    void IterateWaypointIndex()
    {
        waypointIndex++;

        // Check if the current waypoint index triggers the door
        if(useSittingDownWaypoints && !police)
        {
            if (waypointIndex == 1  )
            {
                OpenAndCloseDoor3();
            }

            if (waypointIndex == 4  )
            {
                OpenAndCloseDoor2();
            }

            if (waypointIndex == 10  )
            {
                OpenAndCloseDoor();
            }
        }
        else if(useDownstairsWaypoints && !police)
        {
            
            if (waypointIndex == 5  )
            {
                OpenAndCloseDoor2();
            }
            if (waypointIndex == 7  )
            {
                OpenAndCloseDoor3();
            }
        }
        else
        {

            if (waypointIndex == 3 || waypointIndex == 6 )
            {
                if(!police)
                {
                    OpenAndCloseDoor();
                }
                
            }
        }
        

        if (useSittingDownWaypoints)
        {
            if (waypointIndex == 12)
            {
                agent.isStopped = true;
                destinationReached = true;
                npcAnimator.SetTrigger("IsTrigger");
                return;
            }
        }
        else if (useDownstairsWaypoints)
        {
            if (waypointIndex == 8)
            {
                agent.isStopped = true;
                destinationReached = true;
                npcAnimator.SetTrigger("IsTrigger");
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

    /// <summary>
    /// Opens and closes the first linked door
    /// </summary>
    void OpenAndCloseDoor()
    {
        linkedDoor.OpenDoor();
        StartCoroutine(CloseDoorAfterDelay());
    }

    /// <summary>
    /// Coroutine to close the first linked door after a delay
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        linkedDoor.CloseDoor();
    }

    /// <summary>
    /// Opens and closes the second linked door
    /// </summary>
    void OpenAndCloseDoor2()
    {
        linkedDoor2.OpenDoor();
        StartCoroutine(CloseDoorAfterDelay2());
    }

    /// <summary>
    /// Coroutine to close the second linked door after a delay
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseDoorAfterDelay2()
    {
        yield return new WaitForSeconds(2f);
        linkedDoor2.CloseDoor();
    }

    /// <summary>
    /// Opens and closes the third linked door
    /// </summary>
    void OpenAndCloseDoor3()
    {
        linkedDoor3.OpenDoor();
        StartCoroutine(CloseDoorAfterDelay3());
    }

    /// <summary>
    /// Coroutine to close the third linked door after a delay
    /// </summary>
    /// <returns></returns>
    IEnumerator CloseDoorAfterDelay3()
    {
        yield return new WaitForSeconds(2f);
        linkedDoor3.CloseDoor();
    }

    /// <summary>
    /// Initializes the NavMeshAgent and sets the initial destination
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    /// <summary>
    /// Updates the AI's movement each frame
    /// </summary>
    void Update()
    {
        if (agent.enabled && !agent.isStopped && Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    /// <summary>
    /// Stops the AI from moving
    /// </summary>
    public void StopMoving()
    {
        agent.isStopped = true;
    }

    /// <summary>
    /// Resumes AI movement
    /// </summary>
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

    /// <summary>
    /// Switches to the downstairs waypoint set
    /// </summary>
    public void Downstairs()
    {
        npcAnimator.SetTrigger("IsTrigger");
        StopMoving();
        waypointIndex = 0;
        useSittingDownWaypoints = false;
        useDownstairsWaypoints = true;
        ResumeMoving();
    }

    /// <summary>
    /// Switches to the sitting down waypoint set
    /// </summary>
    public void SittingDown()
    {
        npcAnimator.SetTrigger("IsTrigger");
        StopMoving();
        waypointIndex = 0;
        useDownstairsWaypoints = false;
        useSittingDownWaypoints = true;
        UpdateDestination();
        destinationReached = false;
        agent.isStopped = false;
    }
}




