using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy_Follow : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    /// <summary>
    /// Initializes the component by retrieving required references and preparing the navigation agent for use.
    /// </summary>
    /// <remarks>This method locates the player object in the scene by its tag and assigns its transform for
    /// later use. It also ensures that the navigation agent is positioned correctly on the navigation mesh at the
    /// start. This method is typically called automatically by Unity when the component is enabled.</remarks>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Attempt to find the player object in the scene using its tag and assign its transform to the player variable.
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;

        // Ensure the agent starts on the navigation mesh to prevent navigation issues
        if (agent != null) agent.Warp(transform.position);
    }

    /// <summary>
    /// Updates the agent's navigation state and sets its destination to the player's position if both are available and
    /// the agent is on the navigation mesh.
    /// </summary>
    /// <remarks>If the agent is not currently on the navigation mesh, this method attempts to reposition it
    /// onto the nearest valid location. Once the agent is on the navigation mesh and the player is assigned, the
    /// agent's destination is updated to follow the player. This method is typically called once per frame in a Unity
    /// MonoBehaviour to ensure continuous navigation behavior.</remarks>
    void Update()
    {
        if (agent != null && !agent.isOnNavMesh)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 5.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
        }
        if (agent != null && agent.isOnNavMesh && player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}