using UnityEngine;

public class Crouching : MonoBehaviour
{
    private CharacterController characterController;

    private bool crouch = false;

    private float originalHeight;
    [SerializeField] private float crouchHeight = 0.5f;

    public KeyCode crouchKey = KeyCode.LeftControl;

    void Start()
    {
       characterController = GetComponent<CharacterController>();
       originalHeight = characterController.height;
    }

    void Update()
    {
        if(Input.GetKeyDown(crouchKey))
        {
            crouch = !crouch;
            CheckCrouch();
        }
    }

    void CheckCrouch()
    {
        if(crouch == true)
        {
            characterController.height = crouchHeight;
        }
        else
        {
            characterController.height = originalHeight;
        }
    }
}
