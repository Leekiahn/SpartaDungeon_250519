using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Vector2 curMoveInput;




    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {

    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed * Time.fixedDeltaTime;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    public void onMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }


}
