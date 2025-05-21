using System.Collections;
using UnityEngine;

public class TurretHead : MonoBehaviour
{
    [Header("Turret Head Settings")]
    public float rotationSpeed = 90f;
    private int direction = 1;
    public float rayDistance = 20f;

    public Transform rayPos;
    public LayerMask playerLayer;
    public Transform target;

    private Quaternion originalRotation;
    private float lastSeenTime = Mathf.NegativeInfinity;


    private bool isFindingTarget = true;
    public bool isTracking = false;
    private bool isReturning = false;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // 감지 시도
        Ray ray = new Ray(rayPos.position, rayPos.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, playerLayer))
        {
            target = hit.transform;
            lastSeenTime = Time.time;

            isTracking = true;
            isReturning = false;
        }

        // 타겟 추적
        if (isTracking && target != null)
        {
            LookTarget(target.position);

            // 5초 동안 감지 안 되면 복귀
            if (Time.time - lastSeenTime >= 5f)
            {
                isTracking = false;
                StartCoroutine(ReturnToOrigin());
            }
        }

        // 자동 회전 상태
        else if (!isTracking && !isReturning)
        {
            if (isFindingTarget)
            {
                float rotationAmount = direction * rotationSpeed * Time.deltaTime;
                transform.Rotate(0, rotationAmount, 0);
            }
        }
    }

    private void LookTarget(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0; // 수평만 회전
        if (dir == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator ReturnToOrigin()
    {
        isReturning = true;
        while (Quaternion.Angle(transform.rotation, originalRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = originalRotation;
        isReturning = false;
    }
}
