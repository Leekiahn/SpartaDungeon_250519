using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    equipable,
    consumable,
}

public enum eConsumableType
{
    none,
    Booster,
    DoubleJump,
    Invincibility
}

public enum eEquipableType
{
    none,
    SpeedUp,
    JumpUp,
}

[Serializable]
public class ItemDateConsumable
{
    public eConsumableType type;
    public float value;
    public float duration; // 지속시간
}

[Serializable]
public class ItemDataEquipable
{
    public eEquipableType type;
    public float value; // 효과 수치
}

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public eItemType itemType;
    public GameObject dropPrefab;

    [Header("Consumable")]
    public ItemDateConsumable consumables;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("Equipable")]
    public ItemDataEquipable equipable;
}
