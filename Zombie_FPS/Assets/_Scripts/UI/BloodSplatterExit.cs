using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterExit : MonoBehaviour
{
    public GameObject bloodUI;

    void Start()
    {
        bloodUI.SetActive(false);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            bloodUI.SetActive(false);
        }
    }
}