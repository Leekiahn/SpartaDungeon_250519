using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    private float detectTime;
    public float turretDamange = 5f;
    public float damageTimeRate = 2f;

    private TurretHead turret;
    private GameObject player;
    private PlayerCondition condition;

    private void Start()
    {
        turret = GetComponentInChildren<TurretHead>();
    }

    void Update()
    {
        if (turret.isTracking)
        {
            player = turret.target.gameObject;
            condition = player.GetComponent<PlayerCondition>();

            detectTime += Time.deltaTime;
            if (detectTime > damageTimeRate)
            {
                condition.Hp -= turretDamange;
                detectTime = 0f;
            }
        }
    }
}
