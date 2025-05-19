using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionUI : MonoBehaviour
{
    [SerializeField] private Image hpCircle;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private PlayerCondition playerCondition;

    void Awake()
    {
        if (playerCondition == null)
        {
            Debug.LogError("PlayerCondition component not found on this GameObject.");
            return;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateHpUI();
    }

    void UpdateHpUI()
    {
        hpCircle.fillAmount = playerCondition.hp / playerCondition.maxHp;
        hpText.text = playerCondition.hp.ToString("0");
    }
}
