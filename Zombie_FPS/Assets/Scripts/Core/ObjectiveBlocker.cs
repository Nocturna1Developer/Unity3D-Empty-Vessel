using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBlocker : MonoBehaviour
{
    public GameObject objectiveBlocker;
    public GameObject objectiveBlocker2;

    void Start()
    {
        objectiveBlocker.SetActive(true);
        objectiveBlocker2.SetActive(true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            objectiveBlocker.SetActive(false);
            objectiveBlocker2.SetActive(true);
            
        }
    }
}
