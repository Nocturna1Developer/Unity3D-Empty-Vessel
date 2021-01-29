using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickups : MonoBehaviour
{
    public AudioClip pickupSound;

    private AudioSource audioSource;

    public Slider healthbar;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.playOnAwake = false;
        //audioSource.clip = pickupSound;
    }

    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.tag == "Player")
        {
            PlayerHealth.CurrentHealth += 30;
            healthbar.value += 30;
            audioSource.PlayOneShot(pickupSound);
            //audioSource.Play();
            Destroy(gameObject, .2f);
        }
    }
}
