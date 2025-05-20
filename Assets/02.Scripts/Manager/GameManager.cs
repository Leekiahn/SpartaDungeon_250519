using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameObject player;
    private Vector3 spawnPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Application.targetFrameRate = 60; //������ ����Ʈ ����
    }

    private void Start()
    {
        //�÷��̾ ������ ��ġ�� ����
        spawnPos = player.transform.position;
    }


    private void Update()
    {
        //�÷��̾� ������ y�� -30���� ������ ���� ��ġ�� �̵�
        if (player.transform.position.y < -30f)
        {
            player.transform.position = spawnPos;
        }
    }

    public void MakeManager()
    {
        Debug.Log($"{gameObject.name} is created.");
    }
}
