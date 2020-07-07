using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public GameObject objectiveUI;

    void Start()
    {
        objectiveUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            objectiveUI.SetActive(true);
           
        }
    }
}
