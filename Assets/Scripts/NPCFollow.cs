using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public float followDistance = 2.0f; // The distance the NPC will maintain from the player

    private bool shouldFollow = false; // Flag to determine if the NPC should follow the player

    public Animator npcAnimator;

    void Start()
    {
        // Get the NavMeshAgent component attached to the NPC
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (shouldFollow)
        {
            // Calculate the distance between the NPC and the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // If the player is further away than the follow distance, move towards the player
            if (distanceToPlayer > followDistance)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                // Stop the NPC when within the follow distance
                agent.ResetPath();
            }
        }
    }

    public void Follow()
    {
        // Call this method to start following the player
        npcAnimator.SetTrigger("IsTrigger");
        shouldFollow = true;
    }

    public void StopFollowing()
    {
        // Call this method to stop following the player
        shouldFollow = false;
        agent.ResetPath(); // Stop any ongoing movement
    }
}


