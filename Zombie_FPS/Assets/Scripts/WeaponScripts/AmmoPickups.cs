using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPickups : MonoBehaviour
{
    public AudioClip pickupSound;

    private AudioSource audioSource;

    public GameObject AmmoDisplay;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WeaponOverhaul.bulletsLeft += 32;
            AmmoDisplay.GetComponent<Text>().text = " + 32 Total: " + WeaponOverhaul.bulletsLeft;
            //AmmoDisplay.GetComponent<Text>().text = "" + WeaponOverhaul.bulletsLeft + " / " + WeaponOverhaul.gameObject.currentbullets;
            audioSource.PlayOneShot(pickupSound);
            //audioSource.Play();
            Destroy(gameObject, .2f);
        }
    }

    //private void UpdateAmmoText()
    //{
    //     ammoText.text = " Ammo: " + currentbullets + " / " + bulletsLeft;
    //}

}
