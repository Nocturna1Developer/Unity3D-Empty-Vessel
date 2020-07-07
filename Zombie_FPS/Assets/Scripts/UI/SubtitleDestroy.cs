using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleDestroy : MonoBehaviour
{
    public GameObject subtitleUI;

    void Start()
    {
        subtitleUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            subtitleUI.SetActive(true);
            StartCoroutine(SubtitleDestroyOnEnter());
        }
    }

    IEnumerator SubtitleDestroyOnEnter()
    {
        yield return new WaitForSeconds(10);
        Destroy(subtitleUI);
    }
}
