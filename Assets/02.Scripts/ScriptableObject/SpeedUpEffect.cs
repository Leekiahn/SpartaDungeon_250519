using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEffect : MonoBehaviour, EquipEffect
{
    public void ApplyEffect(ItemData item, float value)
    {
        CharacterManager.Instance.playerCtrl.moveSpeed += value;
    }

    public void RemoveEffect(ItemData item, float value)
    {
        CharacterManager.Instance.playerCtrl.moveSpeed -= value;
    }
}
