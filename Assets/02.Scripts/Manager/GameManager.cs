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
    }

    private void Start()
    {
        spawnPos = player.transform.position;
    }


    private void Update()
    {
        if(player.transform.position.y < -30f)
        {
            player.transform.position = spawnPos;
        }
    }

    public void MakeManager()
    {
        Debug.Log($"{gameObject.name} is created.");
    }
}
