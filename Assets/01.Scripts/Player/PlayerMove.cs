using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    public Vector3 moveDir;
    float speed;
    public float moveSpeed;
    public float crouchSpeed;

    private bool isCrounch = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(x, 0, z).normalized * Time.deltaTime * speed;


        anim.SetFloat("xDir", x);
        anim.SetFloat("zDir", z);
        controller.Move(moveDir);

        if (!isCrounch)
        {
            speed = moveSpeed;
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                anim.SetBool("isCrouch", true);
                isCrounch = true;
            }
        }
        else if(isCrounch)
        {
            speed = crouchSpeed;
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {                
                speed = moveSpeed;
                anim.SetBool("isCrouch", false);
                isCrounch = false;
            }
        }

        if(Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJump");
        }
    }
}




