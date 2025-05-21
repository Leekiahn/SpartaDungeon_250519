using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameObject player;
    private Vector3 spawnPos;


    private void Start()
    {
        //�÷��̾ ������ ��ġ�� ����
        player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ������Ʈ ã��
        spawnPos = player.transform.position;
        Application.targetFrameRate = 60; //������ ����Ʈ ����
    }


    private void Update()
    {
        //�÷��̾� ������ y�� -30���� ������ ���� ��ġ�� �̵�
        if (player.transform.position.y < -30f)
        {
            player.transform.position = spawnPos;
        }
    }
}
