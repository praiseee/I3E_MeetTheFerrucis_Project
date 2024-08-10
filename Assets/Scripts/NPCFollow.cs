/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script handles NPC behavior for following a player in a Unity game. 
 * The NPC will follow the player while maintaining a specified distance. 
 * The script uses the Unity NavMesh system for pathfinding and movement.
 */

using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls NPC following behavior using Unity's NavMeshAgent. The NPC will follow the player while maintaining a specified distance.
/// </summary>
public class NPCFollow : MonoBehaviour
{
    /// <summary>
    /// Reference to the player's transform that the NPC will follow.
    /// </summary>
    public Transform player;

    /// <summary>
    /// Reference to the NavMeshAgent component attached to the NPC.
    /// </summary>
    private NavMeshAgent agent;

    /// <summary>
    /// The distance the NPC will maintain from the player.
    /// </summary>
    public float followDistance = 2.0f;

    /// <summary>
    /// Flag to determine if the NPC should follow the player.
    /// </summary>
    private bool shouldFollow = false;

    /// <summary>
    /// Reference to the NPC's Animator component for triggering animations.
    /// </summary>
    public Animator npcAnimator;

    /// <summary>
    /// Initializes the NPC by getting the NavMeshAgent component.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Updates the NPC's behavior each frame. If following is enabled, the NPC will move towards the player while maintaining the specified distance.
    /// </summary>
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

    /// <summary>
    /// Starts the NPC following the player. Triggers the appropriate animation.
    /// </summary>
    public void Follow()
    {
        npcAnimator.SetTrigger("IsTrigger");
        shouldFollow = true;
    }

    /// <summary>
    /// Stops the NPC from following the player and halts any ongoing movement.
    /// </summary>
    public void StopFollowing()
    {
        shouldFollow = false;
        agent.ResetPath();
    }
}



