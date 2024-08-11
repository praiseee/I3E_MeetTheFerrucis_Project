/*
 * Author: Kishaan S/O Ellapparaja
 * Date: 11/08/2024
 * Description: 
 * This script controls the Grandfather character's movement and animation in the game
 */

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    /// Assign the NavMeshAgent component in the Inspector
    /// </summary>
    public NavMeshAgent agent;

    /// <summary>
    /// Assign the Button GameObject in the Inspector
    /// </summary>
    public Button moveButton;

    private bool shouldMove = false;

    void Start()
    {
        moveButton.gameObject.SetActive(false); //Button is hidden at start
        animator.SetBool("isWalking", false); //Character is not walking at start

        moveButton.onClick.AddListener(StartMoving);
    }

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

    /// <summary>
    /// Starts the Grandfather's movement towards the walkPoint and hides the move button.
    /// </summary>
    public void StartMoving()
    {
        shouldMove = true;
        moveButton.gameObject.SetActive(false); // Hide button after starting to move
    }

    /// <summary>
    /// Shows the move button when the player collides with the Grandfather.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with Grandfather.");
            moveButton.gameObject.SetActive(true); // Show button when player collides
        }
    }

    /// <summary>
    /// Hides the move button when the player exits the collision with the Grandfather.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited collision with Grandfather.");
            moveButton.gameObject.SetActive(false); // Hide button when player leaves
        }
    }
}
