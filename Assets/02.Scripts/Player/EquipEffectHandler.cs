using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipEffectHandler : MonoBehaviour
{
    EquipHandler equipHandler;
    ItemData curEquipItem;


    public SpeedUpEffect speedUpEffect;
    public JumpUpEffect jumpUpEffect;
    public EquipEffect currentEffect;

    void Start()
    {
        equipHandler = GetComponent<EquipHandler>();
        if (equipHandler == null)
        {
            Debug.LogError("EquipHandler component not found on this GameObject.");
            return;
        }
    }

    public void SetEquipEffect(ItemData item)
    {
        switch(item.equipable.type)
        {
            case eEquipableType.SpeedUp:
                curEquipItem = item;
                currentEffect = EquipEffectManager.Instance.speedUpEffect;
                currentEffect.ApplyEffect(curEquipItem, curEquipItem.equipable.value);
                Debug.Log("SpeedUp effect applied with value: " + item.equipable.value);
                break;
            case eEquipableType.JumpUp:
                curEquipItem = item;
                currentEffect = EquipEffectManager.Instance.jumpUpEffect;
                currentEffect.ApplyEffect(curEquipItem, curEquipItem.equipable.value);
                Debug.Log("JumpUp effect applied with value: " + item.equipable.value);
                break;
            default:
                Debug.LogWarning("Unknown equipable type: " + item.equipable.type);
                break;
        }
    }

    public void RemoveEquipEffect()
    {
        currentEffect?.RemoveEffect(curEquipItem, curEquipItem.equipable.value);
        Debug.Log("Equip effect removed.");
    }
}
