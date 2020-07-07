using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject myweapon;
    public GameObject myObjective;
    public GameObject weaponOnGround;

    void Start()
    {
        myweapon.SetActive(false);
        myObjective.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            myweapon.SetActive(true);
            myObjective.SetActive(true);
            weaponOnGround.SetActive(false);
        }
    } 
}
