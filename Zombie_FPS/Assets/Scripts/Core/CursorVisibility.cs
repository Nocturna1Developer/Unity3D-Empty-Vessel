using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class CursorVisibility : MonoBehaviour
{
    void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<FirstPersonController>() != null)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}