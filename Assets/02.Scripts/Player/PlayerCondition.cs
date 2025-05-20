using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public float maxHp = 100f;
    [SerializeField] private float hp;
    public float Hp
    {
        get { return hp; }
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            if (hp <= 0)
            {
                Debug.Log("Player is dead!");
            }
        }
    }


    public float maxStamina = 100f;
    [SerializeField] private float stamina;
    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = Mathf.Clamp(value, 0, maxStamina);
            if (stamina <= 0)
            {
                Debug.Log("Player is out of stamina!");
            }
        }
    }

    public float minSleep = 0f;
    public float maxSleep = 100f;
    [SerializeField] private float sleep;
    public float Sleep
    {
        get { return sleep; }
        set
        {
            sleep = Mathf.Clamp(value, minSleep, maxSleep);
            if (sleep >= 80)
            {
                Debug.Log("Player is too tired!");
            }
        }
    }

    void Awake()
    {
        Hp = maxHp;
        Stamina = maxStamina;
        Sleep = minSleep;
    }

    void Update()
    {
        Stamina += 1f * Time.deltaTime;
        Sleep += 0.5f * Time.deltaTime;
        UIManager.Instance.playerConditionUI.UpdateUI();
    }
}
