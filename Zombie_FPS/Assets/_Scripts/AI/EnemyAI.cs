using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIOverhaul : MonoBehaviour
{
    NavMeshAgent nm;

    HealthController healthController;

    public Transform target;
    public Animator animator;

    public float distanceTreshold = 10f;
    public float attackTreshold = 1.5f;
    
    // Creates a drop-down menu and lets ud determine the state
    public enum AIState { idle, chasing, attack };
    public AIState aiState = AIState.idle;

    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        healthController = GetComponent<HealthController>();
        animator = GetComponent<Animator>();
        
        StartCoroutine(Think());
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);
    }

    // Co-Routine - Zombie adapts to the player location
    public IEnumerator Think()
    {
        while(true)
        {
            switch (aiState)
            {
                case AIState.idle:

                    float dist = Vector3.Distance(target.position, transform.position);

                    if (dist < distanceTreshold) // if dist is less than 10 chase the player
                    {
                       aiState = AIState.chasing; 
                       animator.SetBool("chasing", true);
                    }

                    nm.SetDestination(transform.position);
                    break;

                case AIState.chasing:

                    dist = Vector3.Distance(target.position, transform.position);

                    if (dist > distanceTreshold) // if dist is more than 10 become idle
                    {
                        aiState = AIState.idle;
                        animator.SetBool("chasing", false);
                    }

                    if(dist < attackTreshold) // if dist is less than 1.5 attack player
                    {
                        aiState = AIState.attack;
                        animator.SetBool("attacking", true);
                    }
                    nm.SetDestination(target.position);
                    break;

                case AIState.attack: 
                    //Debug.Log("Attack");
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > attackTreshold) // if dist is more than 1.5 chase player
                    {
                        aiState = AIState.chasing;
                        animator.SetBool("attacking", false);
                    }

                    nm.SetDestination(transform.position);
                    break;

                default:
                    break;
            }

            yield return new WaitForSeconds(.1f);
        }
    }

    void OnTriggerStay(Collider collider) 
    {
        collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
    }


}
