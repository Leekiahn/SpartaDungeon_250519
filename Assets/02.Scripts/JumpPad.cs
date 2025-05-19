using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private GameObject curJumpPlayer;
    [SerializeField] private float upPower;
    [SerializeField] private float forwardPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            curJumpPlayer = other.gameObject;
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                Vector3 jumpDirection = other.transform.up * upPower + other.transform.forward * forwardPower;
                rb.AddForce(jumpDirection, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            curJumpPlayer = null;
        }
    }
}
