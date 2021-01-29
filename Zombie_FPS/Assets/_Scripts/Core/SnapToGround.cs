// using UnityEditor;
// using UnityEngine;

// public class SnapToGround : MonoBehaviour
// {
//     [MenuItem("Custom/Snap To Ground %g")]
//     public static void Ground()
//     {
//         foreach(var transform in Selection.transforms)
//         {
//             var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10f);
//             foreach(var hit in hits)
//             {
//                 if (hit.collider.gameObject == transform.gameObject)
//                     continue;

//                 transform.position = hit.point;
//                 break;
//             }
//         }
//     }

// }