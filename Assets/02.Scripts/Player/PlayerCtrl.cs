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
    [SerializeField] private Vector2 curMoveInput;
    [SerializeField] private LayerMask groundLayer;

    [Header("Look")]
    [SerializeField] private Transform camContainer;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Vector2 curLookInput;
    [SerializeField] private float minXRot = -80f;
    [SerializeField] private float maxXRot = 80f;
    [SerializeField] private float curXRot;
    [SerializeField] private float curYRot;
    [SerializeField] private bool TPSOn = false;


    public Rigidbody _rigidbody;
    private PlayerAnimCtrl _playerAnimCtrl;
    private PlayerInteract playerInteract;
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

        playerInteract = GetComponent<PlayerInteract>();
        if (playerInteract == null)
        {
            Debug.LogError("PlayerInteract component not found on this GameObject.");
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
        Look();
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
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (playerInteract.curInteractObject == null)
            {
                return;
            }
            Destroy(playerInteract.curInteractObject);
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
                cam.transform.localPosition = new Vector3(0.8f, 0f, -3f);
                playerInteract.maxCheckDistance = 6f;
                break;
            case false:
                cam.transform.localPosition = new Vector3(0f, 0f, 0f);
                playerInteract.maxCheckDistance = 3f;
                break;
        }
    }
}
