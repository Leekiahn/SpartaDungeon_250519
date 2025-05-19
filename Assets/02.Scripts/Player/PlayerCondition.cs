using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] public float maxHp = 100f;
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


    [SerializeField] public float maxStamina = 100f;
    [SerializeField] public float stamina;
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

    void Awake()
    {
        Hp = maxHp;
        Stamina = maxStamina;
    }

    void Start()
    {

    }

    void Update()
    {
        Stamina += 1f * Time.deltaTime;
    }
}
