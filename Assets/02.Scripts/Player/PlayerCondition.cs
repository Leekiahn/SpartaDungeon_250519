using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    [SerializeField] public float maxHp = 100f;
    [SerializeField] public float hp;

    void Awake()
    {
        hp = maxHp;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
