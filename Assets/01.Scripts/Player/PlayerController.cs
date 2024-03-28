using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region 플레이어의 Component
    CharacterController controller;
    Animator anim;
    #endregion

    #region 움직임 관련 변수선언
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
    [SerializeField]
    private float gravity = 5f;
    [SerializeField]
    private float jumpPower = 8f;
    #endregion

    #region 플레이어의 Stemina 작용
    [Header("Stamina")]
    public Image steminaUI;
    [SerializeField]
    float maxStemina = 100;
    [SerializeField]
    float curStemina;
    [SerializeField]
    float sprintUse = 2;
    [SerializeField]
    float jumpUse = 10;
    [SerializeField]
    float recoveryIdle = 1;
    [SerializeField]
    float recoveryMove = 0.5f;
    #endregion

    #region 플레이어의 상태 bool체크
    [Header("Bool State")]
    private bool isMove = false;
    private bool isCrouch = false;
    private bool isProne = false;
    private bool isSprint = false;
    private bool isJump = false;
    #endregion

    // TODO : 플레이어의 움직임 안에서 시야 움직임 수정
    [Header("CamPos")]
    public RotateToMouse rotateToMouse;

    [Header("MoveX,Z")]
    float x;
    float z;
    //

    #region Awake,Start 함수
    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isMove = true;
    }

    private void Start()
    {
        maxStemina = 100f;
        curStemina = maxStemina;
        steminaUI.fillAmount = maxStemina / curStemina;
    }
    #endregion

    #region Update 함수
    private void Update()
    {
        // 마우스 움직임에 화면변화
        UpdateRotate();

        // 움직임 구현
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(x, 0, z).normalized * Time.deltaTime * speed;

        Gravity();

        anim.SetFloat("xDir", x);
        anim.SetFloat("zDir", z);
        controller.Move(moveDir);
        //

        // 앉기 상태 변환을 위한 키값 정의
        KeyCode leftC = KeyCode.LeftControl;
        Crouch(leftC);
        //

        // 엎드리는 상태
        Prone();

        // 활동적인 상태
        if (curStemina > 0)
        {
            Activity();
        }
        if(controller.isGrounded)
        {
            isJump = false;
        }

        if (isCrouch) speed = crouchSpeed;
        if (isProne) speed = proneSpeed;
        if (isSprint) speed = sprintSpeed;
        if (isMove) speed = moveSpeed;
    }
    #endregion

    #region 임시 마우스로 화면 전환
    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotateToMouse.CalculateRotation(mouseX, mouseY);
    }
    #endregion

    #region 앉기 상태 변환
    /// <summary>
    /// 특정 키값을 받으면
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

    #region 엎드리는 상태 변환
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

    #region 활동적인 움직임 상태 변환
    private void Activity()
    {
        if ((isMove || isSprint) && controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (isSprint)
                {
                    print("달리기 점프");
                    moveDir.y += jumpPower * 2f;
                    moveDir += transform.forward * jumpPower * 2f;
                }
                else
                {
                    moveDir.y += jumpPower;
                }
                isJump = true;
                anim.SetTrigger("isJump");
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (z > 0 && isMove)
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

    #region 중력 적용
    private void Gravity()
    {
        if (!controller.isGrounded)
        {
            moveDir.y -= gravity * Time.deltaTime;
        }
    }
    #endregion

    #region Stemina 함수
    /// <summary>
    /// 특정 행동마다 회복량 다르게 설정
    /// </summary>
    /// <param name="recovery"></param>
    private void SteminaRecovery(float recovery)
    {
        curStemina += recovery;
        steminaUI.fillAmount = maxStemina / curStemina;
    }

    private void UseStemina(float Use)
    {
        curStemina-= Use;
        steminaUI.fillAmount = maxStemina / curStemina;
    }
    #endregion
}




