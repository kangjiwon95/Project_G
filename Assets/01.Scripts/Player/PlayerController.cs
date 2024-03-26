using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    [Header("Move")]
    private Vector3 moveDir;
    float speed;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float crouchSpeed = 1f;
    [SerializeField]
    private float sprintSpeed = 5f;

    [Header("Crouch State")]
    private bool isCrouch = false;

    [Header("CamPos")]
    public RotateToMouse rotateToMouse;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateRotate();

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(x, 0, z).normalized * Time.deltaTime * speed;


        anim.SetFloat("xDir", x);
        anim.SetFloat("zDir", z);
        controller.Move(moveDir);

        if (!isCrouch)
        {
            speed = moveSpeed;
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                anim.SetBool("isCrouch", true);
                isCrouch = true;
            }
        }
        else if(isCrouch)
        {
            speed = crouchSpeed;
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {                
                speed = moveSpeed;
                anim.SetBool("isCrouch", false);
                isCrouch = false;
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJump");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("isSprint", true);
            speed = sprintSpeed;
        }
        else
        {
            anim.SetBool("isSprint", false);
            speed = moveSpeed;
        }
    }

    void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }
}




