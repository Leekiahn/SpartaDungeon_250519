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
        //�κ��丮�� ���� ���� ���� ī�޶� ȸ��
        if (!UIManager.Instance.invenUI.gameObject.activeSelf)
        {
            Look();
        }
    }

    //�̵�
    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        _rigidbody.MovePosition(_rigidbody.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    //�̵� �Է�
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

    //���� �Է�
    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && CharacterManager.Instance.condition.Stamina >= jumpStamina)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            CharacterManager.Instance.condition.Stamina -= jumpStamina;
        }
    }

    //���� Ȯ��
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

    //ī�޶� ȸ��
    private void Look()
    {
        curXRot += curLookInput.y * mouseSensitivity;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
        camContainer.localEulerAngles = new Vector3(-curXRot, 0f, 0f);

        curYRot += curLookInput.x * mouseSensitivity;
        transform.localEulerAngles = new Vector3(0f, curYRot, 0f);
    }

    //ī�޶� ȸ�� �Է�
    public void OnLookInput(InputAction.CallbackContext context)
    {
        curLookInput = context.ReadValue<Vector2>();
    }

    //�κ��丮 ������Ʈ Ȱ��ȭ ���
    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        UIManager.Instance.invenUI.gameObject.SetActive(!UIManager.Instance.invenUI.gameObject.activeSelf);
        UIManager.Instance.crosshair.SetActive(!UIManager.Instance.invenUI.gameObject.gameObject.activeSelf);
        Cursor.lockState = UIManager.Instance.invenUI.gameObject.activeSelf ? CursorLockMode.None : CursorLockMode.Locked; //�κ��丮�� ������ ũ�ν���� ��
    }

    //�ƾ��� ��ȣ�ۿ� �Է�
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (CharacterManager.Instance.interact.curObject == null)
            {
                return;
            }

            //�κ��丮�� ������ ������
            CharacterManager.Instance.inven.AddItem(CharacterManager.Instance.interact.item);
        }
    }

    //���� ��� ��ȯ
    public void OnViewToggleInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            TPSOn = !TPSOn;
            ViewToggle(TPSOn);
        }
    }

    //���� ī�޶� ��ȯ
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
