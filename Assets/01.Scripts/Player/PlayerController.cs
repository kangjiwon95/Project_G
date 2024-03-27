using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator anim;

    [Header("Move")]
    private Vector3 moveDir;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private float crouchSpeed = 1f;
    [SerializeField]
    private float sprintSpeed = 5f;
    [SerializeField]
    private float proneSpeed = 0.5f;

    [Header("Bool State")]
    private bool isMove = false;
    private bool isCrouch = false;
    private bool isProne = false;
    private bool isSprint = false;

    [Header("CamPos")]
    public RotateToMouse rotateToMouse;

    [Header("MoveX,Z")]
    float x;
    float z;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isMove = true;
    }

    private void Update()
    {
        // ���콺 �����ӿ� ȭ�麯ȭ
        UpdateRotate();

        // ������ ����
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(x, 0, z).normalized * Time.deltaTime * speed;


        anim.SetFloat("xDir", x);
        anim.SetFloat("zDir", z);
        controller.Move(moveDir);
        //

        // �ɱ� ���� ��ȯ�� ���� Ű�� ����
        KeyCode leftC = KeyCode.LeftControl;
        Crouch(leftC);
        //

        // ���帮�� ����
        Prone();

        // Ȱ������ ����
        Activity();

        if(isCrouch) speed = crouchSpeed;
        if(isProne) speed = proneSpeed;
        if(isSprint) speed = sprintSpeed;
        if(isMove) speed = moveSpeed;
    }

    #region �ӽ� ���콺�� ȭ�� ��ȯ
    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }
    #endregion

    #region �ɱ� ���� ��ȯ
    /// <summary>
    /// Ư�� Ű���� ������
    /// </summary>
    /// <param name="leftC"></param>
    private void Crouch(KeyCode leftC)
    {
        if (leftC == KeyCode.LeftControl)
        {
            if (isMove)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    isMove = false;
                    anim.SetBool("isCrouch", true);
                    isCrouch = true;
                }
            }
            else if (isCrouch)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    anim.SetBool("isCrouch", false);
                    isCrouch = false;
                    isMove = true;
                }
            }
        }
    }
    #endregion

    #region ���帮�� ���� ��ȯ
    private void Prone()
    {
        if (isCrouch)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouch = false;
                isProne = true;
                anim.SetBool("isProne", true);
            }
        }
        else if (isProne)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.SetBool("isProne", false);
                isProne = false;
                isCrouch = true;
            }
        }
    }
    #endregion

    #region Ȱ������ ������ ���� ��ȯ
    private void Activity()
    {
        if (isMove || isSprint)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("isJump");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(z > 0 && isMove)
            {
                isSprint = true;
                isMove = false;
                anim.SetBool("isSprint", true);
            }
        }
        if (isSprint && z <= 0)
        {
            isSprint = false;
            isMove = true;
            anim.SetBool("isSprint", false);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprint = false;
            isMove = true;
            anim.SetBool("isSprint", false);
        }
    }
    #endregion
}




