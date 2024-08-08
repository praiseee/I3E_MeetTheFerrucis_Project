using UnityEngine;
using UnityEngine.AI;

public class GrandfatherAI : MonoBehaviour
{
    /// <summary>
    /// Assign the WalkPoint GameObject in Inspector
    /// </summary>
    public GameObject walkPoint;

    /// <summary>
    /// Assign the Animator component in Inspector
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Assign the NavMeshAgent in component in the Inspector
    /// </summary>
    public NavMeshAgent agent; 

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove)
        {
            MoveToWalkPoint();
        }
    }

    /// <summary>
    /// Handle Character movement, move to walkpoint
    /// </summary>
    void MoveToWalkPoint()
    {
        agent.SetDestination(walkPoint.transform.position);
        animator.SetBool("isWalking", true);

        // Stop moving after reaching the destination
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("isWalking", false);
            shouldMove = false;
        }
    }

    public void StartMoving()
    {
        shouldMove = true;
    }
}
