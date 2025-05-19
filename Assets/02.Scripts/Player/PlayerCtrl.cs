using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerCtrl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 60f;
    private float jumpStamina = 5f;
    [SerializeField] private Vector2 curMoveInput;
    [SerializeField] private LayerMask groundLayer;

    [Header("Look")]
    [SerializeField] private Transform camContainer;
    [SerializeField] private GameObject Crosshair;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector2 curLookInput;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    [SerializeField] private float curXRot;
    [SerializeField] private float curYRot;
    [SerializeField] private bool TPSOn = false;
    [SerializeField] private Vector3 TPSPos;


    private Rigidbody _rigidbody;
    private PlayerAnimCtrl _playerAnimCtrl;
    private PlayerInteract _playerInteract;
    private PlayerCondition _playerCondition;

    [SerializeField] private GameObject InventoryUI;
    private Camera cam;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject.");
            return;
        }

        _playerAnimCtrl = GetComponent<PlayerAnimCtrl>();
        if (_playerAnimCtrl == null)
        {
            Debug.LogError("PlayerAnimCtrl component not found on this GameObject.");
            return;
        }

        _playerInteract = GetComponent<PlayerInteract>();
        if (_playerInteract == null)
        {
            Debug.LogError("PlayerInteract component not found on this GameObject.");
            return;
        }

        _playerCondition = GetComponent<PlayerCondition>();
        if (_playerCondition == null)
        {
            Debug.LogError("PlayerCondition component not found on this GameObject.");
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
        if (!InventoryUI.activeSelf)
        {
            Look();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        _rigidbody.MovePosition(_rigidbody.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    public void onMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
            _playerAnimCtrl.playerState = PlayerState.Walk;
        }
        else if (context.canceled)
        {
            curMoveInput = Vector2.zero;
            _playerAnimCtrl.playerState = PlayerState.Idle;
        }
    }

    public void onJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && _playerCondition.Stamina >= jumpStamina)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _playerCondition.Stamina -= jumpStamina;
        }
    }

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

    private void Look()
    {
        curXRot += curLookInput.y * mouseSensitivity;
        curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
        camContainer.localEulerAngles = new Vector3(-curXRot, 0f, 0f);

        curYRot += curLookInput.x * mouseSensitivity;
        transform.localEulerAngles = new Vector3(0f, curYRot, 0f);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        curLookInput = context.ReadValue<Vector2>();
    }

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        InventoryUI.SetActive(!InventoryUI.activeSelf);
        Crosshair.SetActive(!InventoryUI.activeSelf);
        Cursor.lockState = InventoryUI.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (_playerInteract.curInteractObject == null)
            {
                return;
            }
            Destroy(_playerInteract.curInteractObject);
        }
    }

    public void OnViewToggleInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            TPSOn = !TPSOn;
            ViewToggle(TPSOn);
        }
    }

    private void ViewToggle(bool _TPSOn)
    {
        switch (_TPSOn)
        {
            case true:
                cam.transform.localPosition = TPSPos;
                _playerInteract.maxCheckDistance = 6f;
                break;
            case false:
                cam.transform.localPosition = Vector3.zero;
                _playerInteract.maxCheckDistance = 3f;
                break;
        }
    }
}
