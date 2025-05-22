using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHandler : MonoBehaviour
{
    public ItemData curEquipItem;
    public Transform equipPos; // 장비를 장착할 위치

    GameObject equipPrefab;
    public void EquipItem(ItemData itemData)
    {
        if (curEquipItem != null)
        {
            UnequipItem();
        }

        curEquipItem = itemData;
        CharacterManager.Instance.equipEffct.SetEquipEffect(curEquipItem); // 장비 효과 적용
        equipPrefab = Instantiate(itemData.equipPrefab, equipPos.position, Quaternion.identity);
        equipPrefab.transform.position = equipPos.position; // 장비를 장착할 위치에 위치 설정
        equipPrefab.transform.rotation = equipPos.rotation; // 장비를 장착할 위치에 회전 설정
        equipPrefab.transform.localScale = Vector3.one; // 장비의 크기를 원래대로 설정

        equipPrefab.transform.SetParent(equipPos); // 장비를 장착할 위치에 부모로 설정
    }

    public void UnequipItem()
    {
        if (curEquipItem != null)
        {
            CharacterManager.Instance.equipEffct.RemoveEquipEffect(); // 장비 효과 제거
            Destroy(equipPrefab);
            curEquipItem = null;
        }
    }
}
