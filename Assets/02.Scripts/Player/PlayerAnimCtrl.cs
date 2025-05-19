using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Jump,
}

public class PlayerAnimCtrl : MonoBehaviour
{
    public PlayerState playerState = PlayerState.Idle;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                _animator.SetBool("IsMove", false);
                break;
            case PlayerState.Walk:
                _animator.SetBool("IsMove", true);
                break;
            case PlayerState.Jump:
                // Handle jump animation
                break;
        }
    }
}
