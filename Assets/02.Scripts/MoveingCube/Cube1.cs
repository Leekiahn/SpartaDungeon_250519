using UnityEngine;
using System.Collections;

public class Cube1 : MonoBehaviour
{
    public Transform[] pointList; // 지점 리스트
    private Vector3[] pointPos;

    public float waitTime = 2f; // 대기 시간
    public float speed = 2f;

    private int index = 1; // 현재 지점 인덱스
    private Rigidbody rb;
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // pointPos 초기화 및 위치 복사
        pointPos = new Vector3[pointList.Length];
        for (int i = 0; i < pointList.Length; i++)
        {
            pointPos[i] = pointList[i].position;
        }
    }

    void FixedUpdate()
    {
        if (isWaiting) return;

        Vector3 curTarget = pointPos[index];
        Vector3 moveDir = (curTarget - transform.position).normalized;
        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);

        // 도착 처리
        if (Vector3.Distance(transform.position, curTarget) < 0.1f)
        {
            StartCoroutine(WaitAndMoveNext());
        }
    }

    private IEnumerator WaitAndMoveNext()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // 다음 지점 인덱스로 변경 (순환)
        index = (index + 1) % pointPos.Length;
        index = index >= pointPos.Length ? 0 : index; // 인덱스 범위 체크
        isWaiting = false;
    }
}
