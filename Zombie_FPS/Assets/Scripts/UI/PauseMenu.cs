// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine.Rendering.PostProcessing;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityStandardAssets.Characters.FirstPerson;

// public class PauseMenu : MonoBehaviour
// {
//     public static bool GameIsPaused = false;
//     public GameObject pauseMenuUI;
//     public MouseLook mouseLook;

//     void Start() 
//     {
//         mouseLook = GetComponent<MouseLook>();
//     }
//     public void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.P))
//         {
//             if (GameIsPaused)
//             {
//                 Resume();
//             }
//             else
//             {
//                 Pause();
//             }
//         }
//     }

//     // Does the opposite of pause
//     public void Resume()
//     {
//         pauseMenuUI.SetActive(false);
//         Time.timeScale = 1f;
//         GameIsPaused = false;
//         //mouseLook.enabled = true;
//     }

//     // Freezes time and sets the panel in unity to be active
//     void Pause()
//     {
//         pauseMenuUI.SetActive(true);
//         Time.timeScale = 1f;
//         GameIsPaused = false;
//         //GetComponent<MouseLook>() = false;
//     }

    

//     public void LoadMenu()
//     {
//         SceneManager.LoadScene(0);
//     }
// }
