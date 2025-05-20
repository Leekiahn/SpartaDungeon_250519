using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerConditionUI : MonoBehaviour
{
    [SerializeField] private Image hpCircle;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private Image staminaCircle;
    [SerializeField] private TextMeshProUGUI staminaText;

    [SerializeField] private Image sleepCircle;
    [SerializeField] private TextMeshProUGUI sleepText;

    [SerializeField] private PlayerCondition playerCondition;

    void Awake()
    {
        if (playerCondition == null)
        {
            Debug.LogError("PlayerCondition component not found on this GameObject.");
            return;
        }
    }

    public void UpdateUI()
    {
        hpCircle.fillAmount = playerCondition.Hp / playerCondition.maxHp;
        hpText.text = playerCondition.Hp.ToString("0");

        staminaCircle.fillAmount = playerCondition.Stamina / playerCondition.maxStamina;
        staminaText.text = playerCondition.Stamina.ToString("0");

        sleepCircle.fillAmount = playerCondition.Sleep / playerCondition.maxSleep;
        sleepText.text = playerCondition.Sleep.ToString("0");
    }
}
