using UnityEngine;
using System.Collections;

public class Cube1 : MonoBehaviour
{
    public Transform[] pointList; // ���� ����Ʈ
    private Vector3[] pointPos;

    public float waitTime = 2f; // ��� �ð�
    public float speed = 2f;

    private int index = 1; // ���� ���� �ε���
    private Rigidbody rb;
    private bool isWaiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // pointPos �ʱ�ȭ �� ��ġ ����
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

        // ���� ó��
        if (Vector3.Distance(transform.position, curTarget) < 0.1f)
        {
            StartCoroutine(WaitAndMoveNext());
        }
    }

    private IEnumerator WaitAndMoveNext()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        // ���� ���� �ε����� ���� (��ȯ)
        index = (index + 1) % pointPos.Length;
        index = index >= pointPos.Length ? 0 : index; // �ε��� ���� üũ
        isWaiting = false;
    }
}
