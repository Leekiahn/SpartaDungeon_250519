using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EquipEffect
{
    public void ApplyEffect(ItemData item, float value);
    public void RemoveEffect(ItemData item, float value);
}
