using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent naveMeshAgent;

    [SerializeField] public float health = 100f;

    void Start()
    {
        anim = GetComponent<Animator>();
        naveMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void ApplyDamage(float damage)
    {
        //Debug.Log(damage + " points of damage");
        health -= damage;
        if(health <= 0f)
        {
            GetComponent<EnemyAIOverhaul>().enabled = false;
            anim.SetBool("death1", true);
            naveMeshAgent.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
