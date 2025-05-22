using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHandler : MonoBehaviour
{
    public ItemData curEquipItem;
    public Transform equipPos; // ��� ������ ��ġ

    GameObject equipPrefab;
    public void EquipItem(ItemData itemData)
    {
        if (curEquipItem != null)
        {
            UnequipItem();
        }

        curEquipItem = itemData;
        CharacterManager.Instance.equipEffct.SetEquipEffect(curEquipItem); // ��� ȿ�� ����
        equipPrefab = Instantiate(itemData.equipPrefab, equipPos.position, Quaternion.identity);
        equipPrefab.transform.position = equipPos.position; // ��� ������ ��ġ�� ��ġ ����
        equipPrefab.transform.rotation = equipPos.rotation; // ��� ������ ��ġ�� ȸ�� ����
        equipPrefab.transform.localScale = Vector3.one; // ����� ũ�⸦ ������� ����

        equipPrefab.transform.SetParent(equipPos); // ��� ������ ��ġ�� �θ�� ����
    }

    public void UnequipItem()
    {
        if (curEquipItem != null)
        {
            CharacterManager.Instance.equipEffct.RemoveEquipEffect(); // ��� ȿ�� ����
            Destroy(equipPrefab);
            curEquipItem = null;
        }
    }
}
