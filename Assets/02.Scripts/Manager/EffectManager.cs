using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public void PlayEffect(eConsumableType type, float value, float duration)
    {
        switch (type)
        {
            case eConsumableType.Booster:
                StartCoroutine(SpeedBoost(type, value, duration));
                break;
            case eConsumableType.DoubleJump:
                StartCoroutine(DoubleJump(type, value, duration));
                break;
            case eConsumableType.Invincibility:
                StartCoroutine(Invincibility(type, duration));
                break;
            default:
                Debug.LogWarning("Unknown consumable type: " + type + "for" + duration);
                break;
        }
    }

    private IEnumerator SpeedBoost(eConsumableType type, float value, float duration)
    {
        float originSpeed = CharacterManager.Instance.playerCtrl.moveSpeed;
        CharacterManager.Instance.playerCtrl.moveSpeed += value;

        yield return new WaitForSeconds(duration);

        CharacterManager.Instance.playerCtrl.moveSpeed = originSpeed;
    }

    private IEnumerator DoubleJump(eConsumableType type, float value, float duration)
    {
        CharacterManager.Instance.playerCtrl.doubleJumpOn = true;

        yield return new WaitForSeconds(duration);

        CharacterManager.Instance.playerCtrl.doubleJumpOn = false;
    }

    private IEnumerator Invincibility(eConsumableType type, float duration)
    {
        float originHp = CharacterManager.Instance.condition.Hp;
        CharacterManager.Instance.condition.godMode = true;

        yield return new WaitForSeconds(duration);

        CharacterManager.Instance.condition.Hp = originHp; // 원래 체력으로 복구
        CharacterManager.Instance.condition.godMode = false;
    }
}


