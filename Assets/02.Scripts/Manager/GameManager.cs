using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameObject player;
    private Vector3 spawnPos;


    private void Start()
    {
        //플레이어가 스폰될 위치를 저장
        player = GameObject.FindGameObjectWithTag("Player"); //플레이어 오브젝트 찾기
        spawnPos = player.transform.position;
        Application.targetFrameRate = 60; //프레임 레이트 설정
    }


    private void Update()
    {
        //플레이어 포지션 y가 -30보다 작으면 스폰 위치로 이동
        if (player.transform.position.y < -30f)
        {
            player.transform.position = spawnPos;
        }
    }
}
