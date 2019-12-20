using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine;

public class CameraLeaning : MonoBehaviour
{
    private FirstPersonController FPSController;

    private Quaternion initRot;

    private Transform cameraTransform;

    private Vector3 initPos;
    private Vector3 orginalPosition;

    [SerializeField] private float amount = 10f;
    [SerializeField] private float leanSpeed = 2f;

    private bool isLeaningLeft = false;
    private bool isLeaningRight = false;

    public KeyCode leanleftKey = KeyCode.Q;
    public KeyCode leanRightKey = KeyCode.E;

    void Start()
    {
       FPSController = GetComponent<FirstPersonController>();

       cameraTransform = FPSController.transform.GetChild(0);
       initPos = cameraTransform.localPosition;
       initRot = cameraTransform.localRotation;

    }

    void Update()
    {
        //Leaning
        if(Input.GetKey(leanleftKey))
        {
            isLeaningLeft = true;
        }
        else
        {
            isLeaningLeft = false;
        }

        if(Input.GetKey(leanRightKey))
        {
            isLeaningRight = true;
        }
        else
        {
            isLeaningRight = false;
        }

        CheckCanLeanLeft();
        CheckCanLeanRight();
        CheckLeaning();
    }

    void CheckLeaning()
    {
        if(isLeaningLeft)
        {
            FPSController.SetRotateZ(amount);

            Vector3 newPos = new Vector3(initPos.x - 0.5f, initPos.y, initPos.z);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newPos, Time.deltaTime * leanSpeed);
        }
        else if(isLeaningRight)
        {
            FPSController.SetRotateZ(-amount);

            Vector3 newPos = new Vector3(initPos.x + 0.5f, initPos.y, initPos.z);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newPos, Time.deltaTime * leanSpeed);
        }
        else
        {
            FPSController.SetRotateZ(initRot.eulerAngles.z);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, initPos, Time.deltaTime * leanSpeed);
        }
    }

    void CheckCanLeanLeft()
    {
        RaycastHit hit;
        if(Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.left * 0.5f), out hit, 0.5f))
        {
            isLeaningLeft = true;
        }
    }
    
    void CheckCanLeanRight()
    {
        RaycastHit hit;
        if(Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.right * 0.5f), out hit, 0.5f))
        {
            isLeaningRight = true;
        }
    }
}
