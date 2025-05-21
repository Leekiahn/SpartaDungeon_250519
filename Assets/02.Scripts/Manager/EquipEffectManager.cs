using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipEffectManager : Singleton<EquipEffectManager>
{
    public SpeedUpEffect speedUpEffect;
    public JumpUpEffect jumpUpEffect;

    public void Start()
    {
        speedUpEffect = GetComponent<SpeedUpEffect>();
        jumpUpEffect = GetComponent<JumpUpEffect>();
    }
}
