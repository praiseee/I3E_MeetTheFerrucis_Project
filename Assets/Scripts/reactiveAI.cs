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

    void OpenAndCloseDoor2()
    {
        linkedDoor2.OpenDoor();
        StartCoroutine(CloseDoorAfterDelay2());
    }

    IEnumerator CloseDoorAfterDelay2()
    {
        yield return new WaitForSeconds(2f);
        linkedDoor2.CloseDoor();
    }

    void OpenAndCloseDoor3()
    {
        linkedDoor3.OpenDoor();
        StartCoroutine(CloseDoorAfterDelay3());
    }

    IEnumerator CloseDoorAfterDelay3()
    {
        yield return new WaitForSeconds(2f);
        linkedDoor3.CloseDoor();
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
        npcAnimator.SetTrigger("IsTrigger");
        StopMoving();
        waypointIndex = 0;
        useSittingDownWaypoints = false;
        useDownstairsWaypoints = true;
        ResumeMoving();
    }

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




