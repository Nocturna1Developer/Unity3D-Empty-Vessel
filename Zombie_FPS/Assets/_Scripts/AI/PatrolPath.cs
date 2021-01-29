using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class PatrolPath : MonoBehaviour
{  
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    [SerializeField] private float distanceTreshold = 15f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float minSpeed = 3f;

    [SerializeField] private enum AiStates {Patrol, Follow, Attack}
    [SerializeField] AiStates currentState;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int curentWaypoint;

    private void Start()
    {
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        UpdateStates();
    }

    private void UpdateStates()
    {   
        switch (currentState)
        {
            case AiStates.Patrol:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {   
        // If the waypoints are not being followed, then the agent should follow them
        if(agent.destination != waypoints[curentWaypoint].position)
        {
            agent.destination = waypoints[curentWaypoint].position;
        }

        // Incremnting waypoints, getting the length of it and if we reach the end of it, set waypoints to 0 and restart 
        if(hasReached())
        {
            curentWaypoint = (curentWaypoint + 1) % waypoints.Length;
        }

        // Distance From the player and the AI
        float dist = Vector3.Distance(target.position, transform.position);

        // Follow the player if they get too close
        if(dist < distanceTreshold)
        {
            transform.LookAt(target);
            agent.SetDestination(target.position); 
            agent.speed = maxSpeed;
        }
        else
        {
            agent.speed = minSpeed;
        }
    }

    // Returns true if the path is not being persued and if the distance to the path is less than stopping distance
    private bool hasReached()
    {
        return(agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
    }
}