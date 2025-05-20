using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerCtrl : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 60f;
    private float jumpStamina = 5f;
    private Vector2 curMoveInput;
    [SerializeField] private LayerMask groundLayer;

    [Header("Look")]
    [SerializeField] private Transform camContainer;
    [SerializeField] private float mouseSensitivity;
    private Vector2 curLookInput;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    private float curXRot;
    private float curYRot;
    private bool TPSOn = false;
    [SerializeField] private Vector3 TPSPos;


    private Rigidbody _rigidbody;
    private Camera cam;
    public Transform dropPos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject.");
            return;
        }

        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        //인벤토리가 꺼져 있을 때만 카메라 회전
        if (!UIManager.Instance.invenUI.gameObject.activeSelf)
        {
            Look();
        }
    }

    //이동
    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        _rigidbody.MovePosition(_rigidbody.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    //이동 입력
    public void onMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
            CharacterManager.Instance.anim.playerState = PlayerState.Walk;
        }
        else if (context.canceled)
        {
            curMoveInput = Vector2.zero;
            CharacterManager.Instance.anim.playerState = PlayerState.Idle;
        }
    }

    //점프 입력
    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && CharacterManager.Instance.condition.Stamina >= jumpStamina)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            CharacterManager.Instance.condition.Stamina -= jumpStamina;
        }
    }

    //지면 확인
    private bool IsGrounded()
    {
        Ray[] ray = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.right * 0.2f), Vector3.down),
            new Ray(transform.position + (transform.forward * 0.2f) + -(transform.right * 0.2f), Vector3.down),
            new Ray(transform.position + -(transform.forward * 0.2f) + (transform.right * 0.2f), Vector3.down),
            new Ray(transform.position + -(transform.forward * 0.2f) + -(transform.right * 0.2f), Vector3.down),
        };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.2f, groundLayer))
            {
                return true;
            }
        }

        return false;
    }

    //카메라 회전
    private void Look()
    {
        curXRot += curLookInput.y * mouseSensitivity;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
        camContainer.localEulerAngles = new Vector3(-curXRot, 0f, 0f);

        curYRot += curLookInput.x * mouseSensitivity;
        transform.localEulerAngles = new Vector3(0f, curYRot, 0f);
    }

    //카메라 회전 입력
    public void OnLookInput(InputAction.CallbackContext context)
    {
        curLookInput = context.ReadValue<Vector2>();
    }

    //인벤토리 오브젝트 활성화 토글
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        UIManager.Instance.invenUI.gameObject.SetActive(!UIManager.Instance.invenUI.gameObject.activeSelf);
        UIManager.Instance.crosshair.SetActive(!UIManager.Instance.invenUI.gameObject.gameObject.activeSelf);
        Cursor.lockState = UIManager.Instance.invenUI.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked; //인벤토리가 켜지면 크로스헤어 끔
    }

    //아아템 상호작용 입력
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (CharacterManager.Instance.interact.curObject == null)
            {
                return;
            }

            //인벤토리로 아이템 보내기
            CharacterManager.Instance.inven.AddItem(CharacterManager.Instance.interact.item);
        }
    }

    //시점 토글 전환
    public void OnViewToggleInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            TPSOn = !TPSOn;
            ViewToggle(TPSOn);
        }
    }

    //시점 카메라 전환
    private void ViewToggle(bool _TPSOn)
    {
        switch (_TPSOn)
        {
            case true:
                cam.transform.localPosition = TPSPos;
                CharacterManager.Instance.interact.maxCheckDistance = 6f;
                break;
            case false:
                cam.transform.localPosition = Vector3.zero;
                CharacterManager.Instance.interact.maxCheckDistance = 3f;
                break;
        }
    }
}
