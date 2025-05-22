using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpEffect : MonoBehaviour, EquipEffect
{
    public void ApplyEffect(ItemData item, float value)
    {
        CharacterManager.Instance.playerCtrl.jumpForce += value;
    }

    public void RemoveEffect(ItemData item, float value)
    {
        CharacterManager.Instance.playerCtrl.jumpForce -= value;
    }
}
