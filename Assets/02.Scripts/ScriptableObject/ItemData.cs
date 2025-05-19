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
    Booster,
    DoubleJump,
    Invincibility
}

[Serializable]
public class ItemDateConsumable
{
    public eConsumableType type;
    public int value;
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
    public ItemDateConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;

}
