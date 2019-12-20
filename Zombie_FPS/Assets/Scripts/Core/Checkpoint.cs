// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Checkpoint : MonoBehaviour
// {
//     private GameMaster gm;

//     void Start() 
//     {
//         gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
//     }

//     private void OnTriggerEnter(Collider other) 
//     {
//         if(other.CompareTag("Player"))
//         {
//             gm.lastCheckpointPos = transform.position;
//             //Destroy(gameObject);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
           playerHealth.SetSpawnPoint(transform.position);
            //Destroy(gameObject);
        }
    }
}